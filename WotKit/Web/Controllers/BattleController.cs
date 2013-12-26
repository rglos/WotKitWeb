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
    public class BattleController : ApiController
    {
        private WotKitEntities db = new WotKitEntities();

        // GET api/Battle
        public IQueryable<Battle> GetBattles()
        {
            return db.Battles;
        }

        // GET api/Battle/5
        [ResponseType(typeof(Battle))]
        public IHttpActionResult GetBattle(int id)
        {
            Battle battle = db.Battles.Find(id);
            if (battle == null)
            {
                return NotFound();
            }

            return Ok(battle);
        }

        // PUT api/Battle/5
        public IHttpActionResult PutBattle(int id, Battle battle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != battle.BattleId)
            {
                return BadRequest();
            }

            db.Entry(battle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BattleExists(id))
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

        // POST api/Battle
        [ResponseType(typeof(Battle))]
        public IHttpActionResult PostBattle(Battle battle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serverBattle = db.Battles.Where(x => x.ArenaUniqueId == battle.ArenaUniqueId).SingleOrDefault();
            if (serverBattle != null)
            {
                battle = serverBattle;
            }
            else
            {
                db.Battles.Add(battle);
                db.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = battle.BattleId }, battle);
        }

        // DELETE api/Battle/5
        [ResponseType(typeof(Battle))]
        public IHttpActionResult DeleteBattle(int id)
        {
            Battle battle = db.Battles.Find(id);
            if (battle == null)
            {
                return NotFound();
            }

            db.Battles.Remove(battle);
            db.SaveChanges();

            return Ok(battle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BattleExists(int id)
        {
            return db.Battles.Count(e => e.BattleId == id) > 0;
        }
    }
}