using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Web.Domain.Models;

namespace Web.Controllers
{
    public class PlayerBattleController : ApiController
    {
        private WotKitEntities db = new WotKitEntities();

        // GET api/PlayerBattle
        public IQueryable<PlayerBattle> GetPlayerBattles()
        {
            return db.PlayerBattles;
        }

        // GET api/PlayerBattle/5
        [ResponseType(typeof(PlayerBattle))]
        public IHttpActionResult GetPlayerBattle(int id)
        {
            PlayerBattle playerbattle = db.PlayerBattles.Find(id);
            if (playerbattle == null)
            {
                return NotFound();
            }

            return Ok(playerbattle);
        }

        // PUT api/PlayerBattle/5
        public IHttpActionResult PutPlayerBattle(int id, PlayerBattle playerbattle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != playerbattle.PlayerId)
            {
                return BadRequest();
            }

            db.Entry(playerbattle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerBattleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/PlayerBattle
        [ResponseType(typeof(PlayerBattle))]
        public IHttpActionResult PostPlayerBattle(PlayerBattle playerbattle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PlayerBattles.Add(playerbattle);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PlayerBattleExists(playerbattle.PlayerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = playerbattle.PlayerId }, playerbattle);
        }

        // DELETE api/PlayerBattle/5
        [ResponseType(typeof(PlayerBattle))]
        public IHttpActionResult DeletePlayerBattle(int id)
        {
            PlayerBattle playerbattle = db.PlayerBattles.Find(id);
            if (playerbattle == null)
            {
                return NotFound();
            }

            db.PlayerBattles.Remove(playerbattle);
            db.SaveChanges();

            return Ok(playerbattle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlayerBattleExists(int id)
        {
            return db.PlayerBattles.Count(e => e.PlayerId == id) > 0;
        }
    }
}