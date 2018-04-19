using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace CyberShooter
{
    public class GameBoard
    {
        public Player Player { get; set; }
        int screenWidth, screenHeight;
        public static Map map;

        WeaponPickup weaponPickUp;
        ResourcePickup resourcePickUp;
        List<WeaponPickup> weaponPickUpList;
        List<ResourcePickup> resourcePickUpList;
        float playerPickUpDistance, shortestPickUpDistance;
        int pickUpIndex;

        public List<Projectile> projectileList, enemyProjectileList;

        public static int mapHeight = 20;
        public static int mapWidth = 20;
        public static int tileHeight = 32;
        public static int tileWidth = 32;
        public static Vector2 drawOffset = Vector2.Zero;
        public static int drawableLayer = 0;
        string loadFileName = "Text Files\\allHumanTest.txt";
        Vector2 collisionDist = Vector2.Zero;
        Vector2 normal;

        Random rnd = new Random();

        public List<WeaponPickup> GetWeaponPickUpList()
        {
            return weaponPickUpList;
        }
        public List<ResourcePickup> GetResourcePickUpList()
        {
            return resourcePickUpList;
        }
        public GameBoard(int screenWidth, int screenHeight)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            Player = new Player(new Vector2(100, 100));

            //Should be spawned through the map/other system later.
            weaponPickUpList = new List<WeaponPickup>();
            weaponPickUp = new WeaponPickup(new Vector2(100, 500), PickUpTypes.handgun);
            weaponPickUpList.Add(weaponPickUp);
            weaponPickUp = new WeaponPickup(new Vector2(100, 475), PickUpTypes.handgun);
            weaponPickUpList.Add(weaponPickUp);
            weaponPickUp = new WeaponPickup(new Vector2(155, 510), PickUpTypes.rifle);
            weaponPickUpList.Add(weaponPickUp);

            resourcePickUpList = new List<ResourcePickup>();
            resourcePickUp = new ResourcePickup(new Vector2(500, 50), PickUpTypes.rifleAmmo);
            resourcePickUpList.Add(resourcePickUp);
            resourcePickUp = new ResourcePickup(new Vector2(500, 400), PickUpTypes.health);
            resourcePickUpList.Add(resourcePickUp);

            projectileList = new List<Projectile>();
            enemyProjectileList = new List<Projectile>();

            map = new Map(mapWidth, mapHeight, tileWidth, tileHeight);
            map.LoadMap(loadFileName);
            mapHeight = map.mapHeight;
            mapWidth = map.mapWidth;
            tileHeight = map.tileHeight;
            tileWidth = map.tileWidth;

            map = new Map(mapWidth, mapHeight, tileWidth, tileHeight);

            map.LoadMap(loadFileName);

            map.LoadTileSet(Game1.tileSheet);
            map.PopulateHostileHumanLayer();
            map.PopulateFriendlyHumanLayer();
            map.PopulateCollisionLayer();
        }
        public void Update(GameTime gameTime, Vector2 target)
        {
            Player.Update(gameTime, target, this);
            PlayerNPCCollision(gameTime);
            WeaponPickUpSelection();
            WeaponPickupCollection();
            ResourcePickupCollection();
            ProjectileUpdate(projectileList);
            ProjectileUpdate(enemyProjectileList);
            ProjectileNPCCollision();
            NPC(gameTime);
            WallCollision();
            NPCShooting();
            EnemyProjectilePlayerCollision();
        }
        public void NPC(GameTime gameTime)
        {
            for (int i = 0; i < map.NPCs.Count(); i++)
            {
                if (!map.NPCs[i].IsDead)
                {
                    //map.NPCs[i].GetPlayerPos(Player);
                    if (map.NPCs[i].GetDirectionChangeCooldown() <= 0)
                    {
                        map.NPCs[i].SetDirectionX(rnd.Next(map.NPCs[i].GetMinDirectionX(), map.NPCs[i].GetMaxDirectionX()));
                        map.NPCs[i].SetDirectionY(rnd.Next(map.NPCs[i].GetMinDirectionY(), map.NPCs[i].GetMaxDirectionY()));
                        map.NPCs[i].NormalizeDirection();
                        map.NPCs[i].SetDirectionChangeCooldown(rnd.Next(1000, 2000));
                        map.NPCs[i].SetMovementCooldown(rnd.Next(200, 2000));
                        map.NPCs[i].SetVelocity(1f);
                    }
                    map.NPCs[i].Update(gameTime);
                }
            }
        }
        public void WallCollision()
        {
            for (int i = 0; i < map.collisionRects.Count(); i++)
            {
                for (int j = 0; j < map.NPCs.Count(); j++)
                {
                    map.NPCs[j].CollisionCheck(map.collisionRects[i]);
                    if (map.NPCs[j].HitRect.Intersects(map.collisionRects[i]))
                    {
                        map.NPCs[j].SetVelocity(0);
                        map.NPCs[j].Position = map.NPCs[j].OldPosition;
                    }
                }
                if (Player.HitRect.Intersects(map.collisionRects[i]))
                {
                    Player.Position = Player.OldPosition;
                    Player.Speed = Vector2.Zero;
                }
                if (Player.leftRect.Intersects(map.collisionRects[i]))
                    Player.leftRectCollision = true;
                if (Player.rightRect.Intersects(map.collisionRects[i]))
                    Player.rightRectCollision = true;
                if (Player.topRect.Intersects(map.collisionRects[i]))
                    Player.topRectCollision = true;
                if (Player.bottomRect.Intersects(map.collisionRects[i]))
                    Player.bottomRectCollision = true;
                ProjectileWallCollision(i, projectileList);
                ProjectileWallCollision(i, enemyProjectileList);
            }
        }
        public void NPCShooting()
        {
            for (int i = 0; i < map.NPCs.Count(); i++)
            {
                if (Vector2.Distance(Player.Position, map.NPCs[i].Position) <= map.NPCs[i].GetRadius() && map.NPCs[i].GetShootingCooldown() <= 0)
                {
                    Projectile projectile = new Projectile(map.NPCs[i].Position, Player.GetPlayerCenter(), map.NPCs[i].GetDamage(), map.NPCs[i].GetRange(), map.NPCs[i].GetProjectileSpeed());
                    map.NPCs[i].SetShootingCooldown(1000);
                    enemyProjectileList.Add(projectile);
                }
            }
        }
        public void ProjectileUpdate(List<Projectile> projectileList)
        {
            foreach (Projectile projectile in projectileList)
                projectile.Update();

            foreach (Projectile projectile in projectileList)
            {
                if (Vector2.Distance(projectile.GetOriginPosition(), projectile.Position) >= projectile.GetRange())
                {
                    projectileList.Remove(projectile);
                    return;
                }
            }
        }
        public void ProjectileWallCollision(int i, List<Projectile> projectileList)
        {
            foreach(Projectile projectile in projectileList)
            {
                if (projectile.HitRect.Intersects(map.collisionRects[i]))
                {
                    projectileList.Remove(projectile);
                    return;
                }
            }
        }
        public void ProjectileNPCCollision()
        {
            foreach(Projectile projectile in projectileList)
            {
                foreach(NPC npc in map.NPCs)
                {
                    if (projectile.HitRect.Intersects(npc.HitRect))
                    {
                        npc.CurrHealth -= projectile.GetDamage();
                        npc.IsDamaged = true;
                        npc.DamageCooldown = 100;
                        projectileList.Remove(projectile);
                        return;
                    }
                }
            }
        }
        public void EnemyProjectilePlayerCollision()
        {
            foreach(Projectile projectile in enemyProjectileList)
            {
                if (projectile.HitRect.Intersects(Player.HitRect))
                {
                    if (Player.Damage())
                    {
                        Player.CurrHealth = (Player.CurrHealth - projectile.GetDamage());
                    }
                    enemyProjectileList.Remove(projectile);
                    return;
                }
            }
        }
        public void PlayerNPCCollision(GameTime gameTime)
        {
            foreach(NPC npc in map.NPCs)
            {
                if (Player.HitRect.Intersects(npc.HitRect) && !Player.IsDamaged && !npc.IsDead)
                {
                    Player.Damage();
                }
            }
        }
        public void WeaponPickUpSelection()
        {
            shortestPickUpDistance = float.MaxValue;
            pickUpIndex = -1;
            for (int i = 0; i < weaponPickUpList.Count(); i++)
            {
                weaponPickUpList[i].SetIsInteractable(false);
                playerPickUpDistance = Vector2.Distance(Player.GetPlayerCenter(), weaponPickUpList[i].GetPickUpCenter());
                if (playerPickUpDistance < weaponPickUpList[i].GetRadius())
                {
                    if (playerPickUpDistance < shortestPickUpDistance)
                    {
                        shortestPickUpDistance = playerPickUpDistance;
                        pickUpIndex = i;
                    }
                }
            }
            if (pickUpIndex >= 0)
            {
                weaponPickUpList[pickUpIndex].SetIsInteractable(true);
            }
        }
        public void WeaponPickupCollection()
        {
            foreach (WeaponPickup pickUp in weaponPickUpList)
            {
                if (pickUp.GetIsInteractable() && KeyMouseReader.KeyPressed(Keys.E))
                {
                    if (WeaponFullCheck())
                    {
                        WeaponDrop();
                    }
                    pickUp.PickedUp(Player);
                    weaponPickUpList.Remove(weaponPickUpList[pickUpIndex]);
                    return;
                }
            }
        }
        public void ResourcePickupCollection()
        {
            foreach (ResourcePickup pickUp in resourcePickUpList)
            {
                if (pickUp.HitRect.Intersects(Player.HitRect))
                {
                    if (pickUp.PickedUp(Player))
                    {
                        resourcePickUpList.Remove(pickUp);
                        return;
                    }
                }
            }
        }
        public bool WeaponFullCheck()
        {
            if (Player.GetFirstWeapon().GetWeaponName() != WeaponNames.unarmed && Player.GetSecondWeapon().GetWeaponName() != WeaponNames.unarmed)
                return true;
            else return false;
        }
        public void WeaponDrop()
        {
            weaponPickUpList.Add(new WeaponPickup(Player.Position, Player.GetFirstWeapon().GetPickUpType()));
            Player.GetFirstWeapon().SetWeaponName(WeaponNames.unarmed);
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            map.DrawMap(Player, projectileList , enemyProjectileList, weaponPickUpList, resourcePickUpList);
            foreach (WeaponPickup pickUp in GetWeaponPickUpList())
            {
                if (pickUp.GetIsInteractable())
                {
                    spriteBatch.DrawString(Game1.spriteFont, "E", new Vector2(pickUp.Position.X + pickUp.TexWidth / 2, pickUp.Position.Y - 10), Color.Black);
                }
            }
        }
    }
}
