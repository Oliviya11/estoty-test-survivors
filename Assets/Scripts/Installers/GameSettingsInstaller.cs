﻿using System;
using Enemy;
using Enemy.States;
using Misc;
using Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Survivor's-Like Shooter/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public GameInstaller.Settings GameInstaller;
        public GameRestartHandler.Settings GameRestartHandler;
        public PlayerSettings Player;
        public EnemySettings Enemy;
        public EnemySpawner.Settings EnemySpawner;
        
        [Serializable]
        public class PlayerSettings
        {
            public PlayerMoveHandler.Settings PlayerMoveHandler;
            public CameraFollow.Settings CameraFollow;
            public PlayerShootHandler.Settings PlayerShootHandler;
            public PlayerDamageHandler.Settings PlayerDamageHandler;
        }

        [Serializable]
        public class EnemySettings
        {
            public EnemyTunables DefaultSettings;
            public EnemyCommonSettings EnemyCommonSettings;
            public EnemyStateFollow.Settings EnemyStateFollow;
            public EnemyStateAttack.Settings EnemyStateAttack;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(GameInstaller).IfNotBound();
            Container.BindInstance(GameRestartHandler).IfNotBound();
            Container.BindInstance(Player.PlayerMoveHandler).IfNotBound();
            Container.BindInstance(Player.CameraFollow).IfNotBound();
            Container.BindInstance(Player.PlayerShootHandler).IfNotBound();
            Container.BindInstance(Player.PlayerDamageHandler).IfNotBound();
            Container.BindInstance(EnemySpawner).IfNotBound();
            Container.BindInstance(Enemy.EnemyCommonSettings).IfNotBound();
            Container.BindInstance(Enemy.EnemyStateFollow).IfNotBound();
            Container.BindInstance(Enemy.EnemyStateAttack).IfNotBound();
        }
    }
}