﻿using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SampleSecuredWebAPI.Core.Models;
using SampleSecuredWebAPI.WebApi.Models;

namespace SampleSecuredWebAPI.WebApi.Controllers
{
    // TODO WTS: Update or replace this controller as necessary for your needs.
    // Learn more at https://dotnet.microsoft.com/apps/aspnet/apis

    // For more info on claims and role-based autorization see
    // https://docs.microsoft.com/aspnet/core/security/authorization/claims?view=aspnetcore-2.2
    // https://docs.microsoft.com/aspnet/core/security/authorization/roles?view=aspnetcore-2.2
    [Route("api/[controller]")]
    [Authorize(Policy = "SampleClaimPolicy")]
    [Authorize(Roles = "OrderReaders")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<SampleCompany>> List()
        {
            return _itemRepository.GetAll().ToList();
        }

        [HttpGet]
        [Route("~/api/orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<SampleOrder>> ListOrders()
        {
            return _itemRepository.GetAll().SelectMany(c => c.Orders).ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SampleCompany> GetItem(string id)
        {
            var item = _itemRepository.Get(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SampleCompany> Create([FromBody] SampleCompany item)
        {
            _itemRepository.Add(item);
            return CreatedAtAction(nameof(GetItem), new { item.CompanyID }, item);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Edit([FromBody] SampleCompany item)
        {
            try
            {
                _itemRepository.Update(item);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(string id)
        {
            var item = _itemRepository.Remove(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
