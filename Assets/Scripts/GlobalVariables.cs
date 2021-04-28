using UnityEngine;

public static class Global
{
	public static int active_gun = 0; //0 = pistol, 1 = shotgun, 2 = rpg
	public static int unlocked_guns = 0; //1 = shotgun, 2 = rpg, 3 = both
	public static int pistol_ammo = 0;
	public static int shotgun_ammo = 0;
	public static int rpg_ammo = 0;

	public static int pistol_capacity = 8;
	public static int shotgun_capacity = 8;
	public static int rpg_capacity = 1;

	public static int loaded_pistol_ammo = 0;
	public static int loaded_shotgun_ammo = 0;
	public static int loaded_rpg_ammo = 0;

	public const float pistol_reload_time = 2.5f;
	public const float shotgun_reload_time = .5f;
	public const float rpg_reload_time = 6;

	public static int last_pistol_ammo = 0;
	public static int last_shotgun_ammo = 0;
	public static int last_rpg_ammo = 0;
	public static int last_loaded_pistol_ammo = 0;
	public static int last_loaded_shotgun_ammo = 0;
	public static int last_loaded_rpg_ammo = 0;

	public static int game_mode = 0;  //0 = easy, 1 = more difficult, 2 = most difficult
	public static bool crosshair = true;
}


