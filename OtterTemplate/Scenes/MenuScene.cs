using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using OtterTemplate.Utility;

//----------------
// Author: J. Brown (DrMelon)
// Part of the [OtterTemplate] Project.
// Date: 09/03/2016
//----------------
// Purpose: Ultra-basic menu scene. Tool/level select.

namespace OtterTemplate.Scenes
{
    class MenuScene : Scene
    {
        // Storage
        Music MenuMusic;
        Image DebugMenuImage;
        BitmapFont SystemFont;
        RichText MenuTitle;
        RichText MenuPlayDebugLevel;
        RichText MenuSoundTest;
        RichText MenuQuit;

        ControllerXbox360 Player1Controller;

        ParallaxElement parallaxElement;

        int CurrentSelection = 0;
        int MaxSelection = 2;
        float Speed = 1.0f;

        public override void Begin()
        {
            // Load Music
            MenuMusic = new Music(Assets.MUSIC_MENU, true);
            //MenuMusic.Play();

            Game.Color = new Color(135.0f / 255.0f, 206.0f / 255.0f, 235.0f / 255.0f);

            // Load BG
            DebugMenuImage = new Image(Assets.GFX_DEBUGMENU);
            DebugMenuImage.Repeat = true;

            // Load font & text
            SystemFont = new BitmapFont(new Texture(Assets.FNT_SYSTEM), 8, 8, 65);

            MenuTitle = new RichText("{waveAmpY:8}{waveRateY:2}DEBUG MENU", SystemFont, 8, 100, 100);
            MenuTitle.MonospaceWidth = 8;
            MenuTitle.SetPosition(200, 100);
            MenuTitle.CenterOrigin();

            MenuPlayDebugLevel = new RichText("{color:FF0}PLAY GAME", SystemFont, 8, 100, 100);
            MenuPlayDebugLevel.MonospaceWidth = 8;
            MenuPlayDebugLevel.SetPosition(100, 120);

            MenuSoundTest = new RichText("SOUND TEST", SystemFont, 8, 100, 100);
            MenuSoundTest.MonospaceWidth = 8;
            MenuSoundTest.SetPosition(100, 130);

            MenuQuit = new RichText("EXIT", SystemFont, 8, 100, 100);
            MenuQuit.MonospaceWidth = 8;
            MenuQuit.SetPosition(100, 140);

            // Fetch controller
            Player1Controller = Game.Session("Player1").GetController<ControllerXbox360>();

            // Add parallax layers
            parallaxElement = new ParallaxElement();
            parallaxElement.Y = 60;
            parallaxElement.CenterWorldY = 150;

            parallaxElement.AddLayer(new Image(Assets.GFX_PARA_TEST_1), 0, 0);
            parallaxElement.AddLayer(new Image(Assets.GFX_PARA_TEST_2), 5, 0);
            parallaxElement.AddLayer(new Image(Assets.GFX_PARA_TEST_2), 6, 0);
            parallaxElement.AddLayer(new Image(Assets.GFX_PARA_TEST_2), 7, 0);
            parallaxElement.AddLayer(new Image(Assets.GFX_PARA_TEST_2), 8, 0);
            parallaxElement.AddLayer(new Image(Assets.GFX_PARA_TEST_2), 9, 0);
            parallaxElement.AddLayer(new Image(Assets.GFX_PARA_TEST_3), 10, 0);
            parallaxElement.ParallaxLayers.Last().ShouldScale = false;
            parallaxElement.AddLayer(new Image(Assets.GFX_PARA_TEST_4), -1, 0);
            parallaxElement.AddLayer(new Image(Assets.GFX_PARA_TEST_6), 0.1f, 0);
            parallaxElement.AddLayer(new Image(Assets.GFX_PARA_TEST_6), 1.0f, 0);
            parallaxElement.AddLayer(new Image(Assets.GFX_PARA_TEST_6), 1.5f, 0);
            parallaxElement.AddLayer(new Image(Assets.GFX_PARA_TEST_6), 2.0f, 0);
            parallaxElement.AddLayer(new Image(Assets.GFX_PARA_TEST_6), 2.5f, 0);
            parallaxElement.AddLayer(new Image(Assets.GFX_PARA_TEST_6), 3.0f, 0);
            parallaxElement.AddLayer(new Image(Assets.GFX_PARA_TEST_6), 3.5f, 0);
            parallaxElement.AddLayer(new Image(Assets.GFX_PARA_TEST_6), 4.0f, 0);
            parallaxElement.AddLayer(new Image(Assets.GFX_PARA_TEST_6), 4.5f, 0);

            Add(parallaxElement);

            //AddGraphic(DebugMenuImage);
            //AddGraphic(MenuTitle);
            //AddGraphic(MenuPlayDebugLevel);
            //AddGraphic(MenuSoundTest);
            //AddGraphic(MenuQuit);
            
        }

        public override void Update()
        {
            base.Update();

            // Scroll BG
            DebugMenuImage.X += 1;
            DebugMenuImage.Y -= 1;


            // Menu Controls
            CameraX += Speed;

            if(Player1Controller.LeftStick.Up.Down)
            {
                CameraY -= 1f;
            }

            if (Player1Controller.LeftStick.Right.Pressed)
            {
                if (Speed < 20.0f)
                {
                    Speed *= 1.25f;
                }

            }

            if (Player1Controller.LeftStick.Left.Pressed)
            {
                
                if(Speed > 1.0f)
                {
                    Speed *= 0.75f;
                }

            }


            if (Player1Controller.LeftStick.Down.Down)
            {
                CameraY += 1f;

            }

            if(Player1Controller.Start.Pressed)
            {
                parallaxElement.Scale = !(parallaxElement.Scale);
                parallaxElement.UpdateLayers();
            }

            if(parallaxElement.Scale)
            {
                Util.Log("Perspective Correction: ON");
            }
            else
            {
                Util.Log("Perspective Correction: OFF");
            }

            Util.Log("XSpeed: {0}", Speed);
            Util.Log("YPos: {0}", CameraY);
            Util.Log("XPos: {0}", CameraX);

        }

        public void CheckSelection()
        {
            switch (CurrentSelection)
            {
                case 0:
                    MenuPlayDebugLevel.String = "{color:FF0}PLAY GAME";
                    MenuSoundTest.String = "SOUND TEST";
                    MenuQuit.String = "EXIT";
                    break;
                case 1:
                    MenuPlayDebugLevel.String = "PLAY TEST LEVEL";
                    MenuSoundTest.String = "{color:FF0}SOUND TEST";
                    MenuQuit.String = "EXIT";
                    break;
                case 2:
                    MenuPlayDebugLevel.String = "PLAY TEST LEVEL";
                    MenuSoundTest.String = "SOUND TEST";
                    MenuQuit.String = "{color:FF0}EXIT";
                    break;
            }
        }

        public void DoSelection()
        {
            switch (CurrentSelection)
            {
                case 0:
                    // Load Game
                    //Game.AddScene();
                    break;
                case 1:
                    // Load Sound Test Screen
                    //
                    break;
                case 2:
                    // Quit
                    Game.Close();
                    break;
            }
        }

    }
}
