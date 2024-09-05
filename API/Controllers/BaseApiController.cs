using API.RequestHelpers;
using Core.Entites;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController: ControllerBase
    {
        protected async Task<ActionResult> CreatePageResult <T>(IGenericRepository<T> repo, 
        ISpecefication<T> spec ,int pageIndex , int pageSize) where T : BaseEntity
        {
            var items = await repo.ListAsync(spec);
            var count = await repo.CountAsync(spec);
             var pagination = new Pagination<T>(pageIndex,pageSize,count,items);

             return Ok(pagination);
        }
    }
}