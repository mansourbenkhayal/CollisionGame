using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollisionGame
{
    enum SpriteToDraw {
        CrapFace = 1,
        Player = 2,
        Fireball = 3,
    }


    // abstract class is just a structure class and it has to be implemented in the other class that is inheareted from
    // abstract class inforce the coder to implement whenever they are inheareted from this class
    // abstract class can not be instaintiated example: var object = new GameObject is illegal and can't be compiled.
    
    abstract class GameObject
    {
        // properties of the gameobject
        public float x { get; set; }
        public float y { get; set; }
        public abstract void Tick(float currentTime, float dt, GameLogic.AddNewObjectDelegate fnAddNew);
        public abstract SpriteToDraw GetSprite();
        
    }
    class ObstacleObject : GameObject
    {
        static Random r = new Random();
        public ObstacleObject()
        {
            // Starting position on the x axiss
            x = (float)(r.NextDouble() * 500);
            // Starting position on the y axiss
            y = (float)(r.NextDouble() * 500);
        }
        public override void Tick(float currentTime, float dt, GameLogic.AddNewObjectDelegate fnAddNew)
        {

        }
        public override SpriteToDraw GetSprite()
        {
            return SpriteToDraw.CrapFace;
        }
    }
    // playerobject class inherited from gameobject
    class PlayerObject : GameObject
    {
        // player speed
        private float playerspeed = 75;
        // implementing the overrde Tick function from the GameObject class
        public override void Tick(float currentTime, float dt, GameLogic.AddNewObjectDelegate fnAddNew)
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
        public override SpriteToDraw GetSprite()
        {
            return SpriteToDraw.Player;
        }
        public bool left { get; set; }
        public bool right { get; set; }
        public bool up { get; set; }
        public bool down { get; set; }
    }
    class Enemy : ObstacleObject
    {
        private float lastfireballtime=0;

        private int enamyspeed = 50;
        private GameObject target;
        public Enemy(GameObject p)
        {
            this.target = p;
        }
        // overriding Tick to change the implementation from the inhireted from GameObject Class
        public override void Tick(float currentTime, float dt, GameLogic.AddNewObjectDelegate fnAddNew)
        {
            float deltaX = this.x - target.x;
            float deltaY = this.y - target.y;
            if (deltaX > 0.5)
            {
                x -= dt * enamyspeed;
            }
            else if (deltaX < -0.5)
            {
                x += dt * enamyspeed;
            }
            else if (deltaY > 0.5)
            {
                y -= dt * enamyspeed;
            }
            else if (deltaY < -0.5)
            {
                y += dt * enamyspeed;
            }
            float lasttime;
            lasttime = currentTime - lastfireballtime;
            if (lasttime>1)
            {
                fnAddNew(new Fireball(this.x, this.y));
                lastfireballtime = currentTime;
            }
            
        }

    }
    class Fireball: GameObject
    {
        public Fireball(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public override void Tick(float currentTime, float dt, GameLogic.AddNewObjectDelegate fnAddNew)
        {
            int fireBallSpeed;
            fireBallSpeed = 100;

            x += dt * fireBallSpeed;
        }
        public override SpriteToDraw GetSprite()
        {
            return SpriteToDraw.Fireball;
        }
    }
    class GameLogic
    {
        public delegate void AddNewObjectDelegate(GameObject o);


        public List<GameObject> all { get; }
        public PlayerObject player { get; }
        public bool isDead { get; set; }
        // constructor
        public GameLogic()
        {
            // initilizing isDead to false at the start of the Game
            isDead = false;
            // creating all to a list for the enemy
            // intilizied to empty list and not null object
            all = new List<GameObject>();
            // Creating a Player object
            player = new PlayerObject();
            // Adding a player
            all.Add(player);
            // Not Sure about this loop!!!
            for (int x = 0; x < 10; x++)
            {
                all.Add(new ObstacleObject());
            }
            // Creating Multiple enemy.. in this case 10
            for (int x = 0; x < 10; x++)
            {
                all.Add(new Enemy(player));
            }



        }

        private void AddNewObject(GameObject o)
        {
            // this is the implementation of the GameLogic.AddNewObjectDelegate delegate.
            // this gets called from various Tick() functions when a GameObject wants to add another GameObject to the playing field
            all.Add(o);
        }

        public void tick(float t, float dt)
        {
            // check for collisions between all our objects and the player
            collide();

            // go through every single game object and get them to update their position/logic/etc
            for (int x = 0; x < all.Count; x++)
            {
                all[x].Tick(t, dt, AddNewObject);
            }
        }
        // collide funtion to calculate the collition 
        private void collide()
        {

            for (int i = 0; i < all.Count; i++)
            {
                GameObject go = all[i];
                if (go == player)
                {
                    continue;
                }
                float dx;
                float dy;
                float r;
                dx = this.player.x - go.x;
                dy = this.player.y - go.y;
                r = (float)Math.Sqrt((dx * dx) + (dy * dy));

                if (r < 10)
                {
                    isDead = true;
                    Console.WriteLine(" You lost!");

                }
            }

    
           
        }
    }
}
