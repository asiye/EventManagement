using EventManagement.Domain.Entities;
using EventManagement.Domain.Interfaces;
using Nethereum.BlockchainProcessing.BlockStorage.Repositories;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Services;
public class BlockchainExplorerService
{
    private readonly Web3 _web3;
    private readonly ITransactionRecordRepository _repository;
    public BlockchainExplorerService(Web3 web3, ITransactionRecordRepository repository)
    {
        _repository = repository;
        _web3 = web3;
    }

    public async Task<BlockWithTransactions> GetBlockDetailsAsync(ulong blockNumber)
    {
        return await _web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(new Nethereum.Hex.HexTypes.HexBigInteger(blockNumber));
    }

    public async Task<TransactionReceipt> GetTransactionDetailsAsync(string txHash)
    {
        return await _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(txHash);
    }

    public async Task<List<Transaction>> GetTransactionsForBlockAsync(ulong blockNumber)
    {
        var block = await GetBlockDetailsAsync(blockNumber);
        var transactions = new List<Transaction>();

        foreach (var tx in block.Transactions)
        {
            transactions.Add(await _web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(tx.TransactionHash));
        }

        return transactions;
    }

    public async Task CacheTransactionDetailsAsync(TransactionReceipt transactionReceipt)
    {
        var entity = new TransactionRecord
        {
            TranasactionHash = transactionReceipt.TransactionHash,
            BlockHash = transactionReceipt.BlockHash,
            BlockNumber = transactionReceipt.BlockNumber.Value,
            From = transactionReceipt.From,
            To = transactionReceipt.To,
            Value = transactionReceipt.GasUsed.Value,
            Timestamp = DateTime.UtcNow
        };
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
    }

    public async Task<List<Transaction>> GetTransactionsByAddressAsync(ulong startBlock, ulong endBlock)
    {
        var transactions = await Task.WhenAll(
            Enumerable.Range((int)startBlock, (int)(endBlock - startBlock + 1))
                      .Select(async blockNumber =>
                      {
                          var block = await _web3.Eth.Blocks.GetBlockWithTransactionsByNumber
                                             .SendRequestAsync(new BlockParameter((ulong)blockNumber));
                          return block.Transactions;
                      })
        );

        var allTransactions = transactions.SelectMany(t => t).ToList();

        return allTransactions;
    }

    public async Task<List<TransactionRecord>> GetTransactionsByAddressAsync(string address)
    {
        return (await _repository.GetTransactionsByAddressAsync(address)).ToList();

    }
}
