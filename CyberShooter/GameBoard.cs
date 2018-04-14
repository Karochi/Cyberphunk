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
        Player player;
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

        public Player GetPlayer()
        {
            return player;
        }
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
            player = new Player(new Vector2(100, 100));

            //Should be spawned through the map/other system later.
            weaponPickUpList = new List<WeaponPickup>();
            weaponPickUp = new WeaponPickup(new Vector2(100, 500), PickUpTypes.handgun);
            weaponPickUpList.Add(weaponPickUp);
            weaponPickUp = new WeaponPickup(new Vector2(100, 475), PickUpTypes.handgun);
            weaponPickUpList.Add(weaponPickUp);
            weaponPickUp = new WeaponPickup(new Vector2(155, 510), PickUpTypes.rifle);
            weaponPickUpList.Add(weaponPickUp);

            resourcePickUpList = new List<ResourcePickup>();
            resourcePickUp = new ResourcePickup(new Vector2(500, 50), PickUpTypes.ammo);
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
            player.Update(gameTime, target, this);
            PlayerNPCCollision(gameTime);
            WeaponPickUpSelection();
            WeaponPickupCollection();
            ResourcePickupCollection();
            ProjectileUpdate(projectileList);
            ProjectileUpdate(enemyProjectileList);
            ProjectileNPCCollision();
            NPC(gameTime);
            NPCCollision();
            NPCShooting();
            EnemyProjectilePlayerCollision();
        }
        public void NPC(GameTime gameTime)
        {
            for (int i = 0; i < map.NPCs.Count(); i++)
            {
                if (!map.NPCs[i].GetIsDead())
                {
                    //map.NPCs[i].GetPlayerPos(player);
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
        public void NPCCollision()
        {
            for (int i = 0; i < map.collisionRects.Count(); i++)
            {
                for (int j = 0; j < map.NPCs.Count(); j++)
                {
                    map.NPCs[j].CollisionCheck(map.collisionRects[i]);
                    if (map.NPCs[j].GetHitRect().Intersects(map.collisionRects[i]))
                    {
                        map.NPCs[j].SetVelocity(0);
                        map.NPCs[j].SetPosition(map.NPCs[j].GetOldPosition());
                    }
                }
                if (player.GetHitRect().Intersects(map.collisionRects[i]))
                {
                    player.SetPosition(player.GetOldPosition());
                    player.SetSpeed(Vector2.Zero);
                }
                ProjectileWallCollision(i, projectileList);
                ProjectileWallCollision(i, enemyProjectileList);
            }
        }
        public void NPCShooting()
        {
            for (int i = 0; i < map.NPCs.Count(); i++)
            {
                if (Vector2.Distance(GetPlayer().GetPosition(), map.NPCs[i].GetPosition()) <= map.NPCs[i].GetRadius() && map.NPCs[i].GetShootingCooldown() <= 0)
                {
                    Projectile projectile = new Projectile(map.NPCs[i].GetPosition(), GetPlayer().GetPlayerCenter(), map.NPCs[i].GetDamage(), map.NPCs[i].GetRange(), map.NPCs[i].GetProjectileSpeed());
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
                if (Vector2.Distance(projectile.GetOriginPosition(), projectile.GetPosition()) >= projectile.GetRange())
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
                if (projectile.GetHitRect().Intersects(map.collisionRects[i]))
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
                    if (projectile.GetHitRect().Intersects(npc.GetHitRect()))
                    {
                        npc.SetCurrHealth(npc.GetCurrHealth() - projectile.GetDamage());
                        npc.SetIsDamaged(true);
                        npc.SetDamageCooldown(100);
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
                if (projectile.GetHitRect().Intersects(GetPlayer().GetHitRect()))
                {
                    if (GetPlayer().Damage())
                    {
                        GetPlayer().SetCurrHealth(GetPlayer().GetCurrHealth() - projectile.GetDamage());
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
                if (player.GetHitRect().Intersects(npc.GetHitRect()) && !player.GetIsDamaged() && !npc.GetIsDead())
                {
                    player.Damage();
                    //player.SetPosition(player.GetOldPosition());
                    //player.SetSpeed(new Vector2(0, 0));
                    //npc.SetSpeed(new Vector2(0, 0));
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
                playerPickUpDistance = Vector2.Distance(player.GetPlayerCenter(), weaponPickUpList[i].GetPickUpCenter());
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
                    pickUp.PickedUp(player);
                    weaponPickUpList.Remove(weaponPickUpList[pickUpIndex]);
                    return;
                }
            }
        }
        public void ResourcePickupCollection()
        {
            foreach (ResourcePickup pickUp in resourcePickUpList)
            {
                if (pickUp.GetHitRect().Intersects(player.GetHitRect()))
                {
                    pickUp.PickedUp(player);
                    resourcePickUpList.Remove(pickUp);
                    return;
                }
            }
        }
        public bool WeaponFullCheck()
        {
            if (player.GetFirstWeapon().GetWeaponName() != WeaponNames.unarmed && player.GetSecondWeapon().GetWeaponName() != WeaponNames.unarmed)
                return true;
            else return false;
        }
        public void WeaponDrop()
        {
            weaponPickUpList.Add(new WeaponPickup(GetPlayer().GetPosition(), GetPlayer().GetFirstWeapon().GetPickUpType()));
            GetPlayer().GetFirstWeapon().SetWeaponName(WeaponNames.unarmed);
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            map.DrawMap(player, projectileList , enemyProjectileList, weaponPickUpList, resourcePickUpList);
        }
    }
}
