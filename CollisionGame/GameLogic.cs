using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollisionGame
{
    abstract class GameObject
    {
        public float x { get; set; }
        public float y { get; set; }
        public abstract void Tick(float dt);
    }
    class ObstacleObject : GameObject
    {
        static Random r = new Random();
        public ObstacleObject()
        {
            x = (float)(r.NextDouble() * 500);
            y = (float)(r.NextDouble() * 500);
        }
        public override void Tick(float dt )
        {

        }
    }
    class PlayerObject : GameObject 
    {
        private float playerspeed = 100;
        public override void Tick(float dt)
        {
            if (this.left)
            {
                x -= dt * playerspeed;
            }
            if (this.right)
            {
                x += dt * playerspeed;
            }
            if (this.up)
            {
                y -= dt * playerspeed;
            }
            if (this.down)
            {
                y += dt * playerspeed;
            }
        }
        public bool left { get; set; }
        public bool right { get; set; }
        public bool up { get; set; }
        public bool down { get; set; }
    }
    class GameLogic
    {
        public List<GameObject> all { get; }
        public PlayerObject player { get; }
        public GameLogic()
        {
            all = new List<GameObject>();
            player = new PlayerObject();
            all.Add(player);
            for(int x=0; x<10; x++)
            {
                all.Add(new ObstacleObject());
            }
          
        }
        public void tick(float dt)
        {

            for(int x=0; x<all.Count; x++)
            {
                all[x].Tick(dt);
            }
        }
    }
}
