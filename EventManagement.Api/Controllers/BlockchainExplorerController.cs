using EventManagement.Api.Hubs;
using EventManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EventManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BlockchainExplorerController : ControllerBase
{
    private readonly BlockchainExplorerService _explorerService;
    private readonly IHubContext<BlockchainHub> _blockchainHub;

    public BlockchainExplorerController(BlockchainExplorerService explorerService, IHubContext<BlockchainHub> blockchainHub)
    {
        _explorerService = explorerService;
        _blockchainHub = blockchainHub;
    }

    [HttpGet("block/{blockNumber}")]
    public async Task<IActionResult> GetBlockDetails(ulong blockNumber)
    {
        var block = await _explorerService.GetBlockDetailsAsync(blockNumber);

        return Ok(block);
    }

    [HttpGet("transaction/{txHash}")]
    public async Task<IActionResult> GetTransactionDetails(string txHash)
    {
        var transaction = await _explorerService.GetTransactionDetailsAsync(txHash);
        return Ok(transaction);
    }

    [HttpGet("transactions/byAddress/{address}")]
    public async Task<IActionResult> GetTransactionsByAddress(string address)
    {
        var transactions = await _explorerService.GetTransactionsByAddressAsync(address);
        return Ok(transactions);
    }

    [HttpGet("transactions/byBlockRange")]
    public async Task<IActionResult> GetTransactionsByBlockRange([FromQuery] ulong startBlock, [FromQuery] ulong endBlock)
    {
        var transactions = await _explorerService.GetTransactionsByAddressAsync(startBlock, endBlock);
        return Ok(transactions);
    }
}

