using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShockAPI;
using TShockAPI.DB;
using Terraria;
using TerrariaApi.Server;

namespace TshockRemoveRememberedposOnDeath
{
    [ApiVersion(2, 1)]
    public class TshockRemoveRememberedposOnDeath : TerrariaPlugin
    {
        /// <summary>
        /// Gets the author(s) of this plugin
        /// </summary>
        public override string Author
        {
            get { return "utsugiriso"; }
        }

        /// <summary>
        /// Gets the description of this plugin.
        /// A short, one lined description that tells people what your plugin does.
        /// </summary>
        public override string Description
        {
            get { return "Remove rememberedpos on death for hardcore."; }
        }

        /// <summary>
        /// Gets the name of this plugin.
        /// </summary>
        public override string Name
        {
            get { return "RemoveRememberedposOnDeath Plugin"; }
        }

        /// <summary>
        /// Gets the version of this plugin.
        /// </summary>
        public override Version Version
        {
            get { return new Version(0, 0, 0, 1); }
        }

        /// <summary>
        /// Initializes a new instance of the TestPlugin class.
        /// This is where you set the plugin's order and perfrom other constructor logic
        /// </summary>
        public TshockRemoveRememberedposOnDeath(Main game) : base(game)
        {

        }

        /// <summary>
        /// Handles plugin initialization. 
        /// Fired when the server is started and the plugin is being loaded.
        /// You may register hooks, perform loading procedures etc here.
        /// </summary>
        public override void Initialize()
        {
            TShockAPI.Hooks.PlayerHooks.PlayerLogout += OnPlayerLogout;
        }

        /// <summary>
        /// Handles plugin disposal logic.
        /// *Supposed* to fire when the server shuts down.
        /// You should deregister hooks and free all resources here.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Deregister hooks here
            }
            base.Dispose(disposing);
        }

        private void OnPlayerLogout(TShockAPI.Hooks.PlayerLogoutEventArgs args)
        {
            if (TShock.Config.RememberLeavePos && args.Player.Dead)
            {
                try
                {
                    TShock.DB.Query("DELETE FROM RememberedPos WHERE Name=@0 AND WorldID=@1;", args.Player.Name, Main.worldID.ToString());
                }
                catch (Exception ex)
                {
                    TShock.Log.Error(ex.ToString());
                }
            }
        }
    }
}
