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
        private float playerspeed = 10;
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
    class Enemy:ObstacleObject
    {
        private int enamyspeed = 50;
        private GameObject target;
        public Enemy(GameObject p)
        {
            this.target = p;
        }
        
        public override void Tick(float dt)
        {
            float deltaX = this.x - target.x;
            float deltaY = this.y - target.y;
            if(deltaX > 0.5)
            {
                x -= dt * enamyspeed;
            }
            else if (deltaX < -0.5)
            {
                x += dt * enamyspeed;
            }
            else if(deltaY > 0.5)
            {
                y -= dt * enamyspeed;
            }
            else if(deltaY < -0.5)
            {
                y += dt * enamyspeed;
            }
        }

    }
    class GameLogic
    {
        public List<GameObject> all { get; }
        public PlayerObject player { get; }
        public bool isDead { get; set; }
        public GameLogic()
        {
            isDead = false;
            all = new List<GameObject>();
            player = new PlayerObject();
            all.Add(player);
            for(int x=0; x<10; x++)
            {
                all.Add(new ObstacleObject());
            }
            for (int x = 0; x < 10; x++)
            {
                //all.Add(new Enemy(player));
            }
            
        }
        public void tick(float dt)
        {
            collide();
            for(int x=0; x<all.Count; x++)
            {
                all[x].Tick(dt);
            }
        }

        private void collide ()
        {
           
            for(int i = 0; i<all.Count; i++)
            {
                GameObject go = all[i];
                if(go == player)
                {
                    continue;
                }
                float dx;
                float dy;
                float r;
                dx = this.player.x - go.x;
                dy = this.player.y - go.y;
                r = (float)Math.Sqrt((dx * dx) + (dy * dy));

                if(r<10)
                {
                    isDead = true;
                    Console.WriteLine(" You lost!");
                }
            }

            
        }
    }
}
