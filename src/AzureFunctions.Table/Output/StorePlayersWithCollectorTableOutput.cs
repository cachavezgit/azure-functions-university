using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using AzureFunctions.Models;

namespace AzureFunctions.Table.Output
{
    public static class StorePlayersWithCollectorTableOutput
    {
        [FunctionName(nameof(StorePlayersWithCollectorTableOutput))] 
        public static async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                nameof(HttpMethods.Post),
                Route = null)] PlayerEntity[] playerEntities,
            [Table(TableConfig.Table)] IAsyncCollector<PlayerEntity> collector)
        {
            foreach (var playerEntity in playerEntities)
            {
                playerEntity.SetKeys();
                await collector.AddAsync(playerEntity);
            }

            return new AcceptedResult();
        }
    }
}
