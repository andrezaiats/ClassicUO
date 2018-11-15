﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ClassicUO.Configuration;
using ClassicUO.Game.GameObjects;
using ClassicUO.Game.Gumps.Controls;
using ClassicUO.IO.Resources;
using ClassicUO.Renderer;

using Microsoft.Xna.Framework;

namespace ClassicUO.Game.Gumps.UIGumps
{
    class OptionsGump1 : Gump
    {
        private readonly Settings _settings;

        public OptionsGump1() : base(0, 0)
        {

            _settings = Service.Get<Settings>();


            AddChildren(new ResizePic(/*0x2436*/ /*0x2422*/ /*0x9C40*/ 9200 /*0x53*/ /*0xE10*/) { Width = 600, Height = 500 } );

            //AddChildren(new GameBorder(0, 0, 600, 400, 4));

            //AddChildren(new GumpPicTiled(4, 4, 600 - 8, 400 - 8, 0x0A40) { IsTransparent = false});

            //AddChildren(new ResizePic(0x2436) { X = 20, Y = 20, Width = 150, Height = 460 });

            //AddChildren(new LeftButton() { X = 40, Y = 40 });

            ScrollArea leftArea = new ScrollArea(10, 10, 160, 480, true);

            leftArea.AddChildren(new Button(0, 0x9C5, 0x9C5, 0x9C5, "General", 1, true, 14, 24)  {  FontCenter = true, ButtonAction = ButtonAction.SwitchPage, ToPage = 1});
            leftArea.AddChildren(new Button(0, 0x9C5, 0x9C5, 0x9C5, "Sounds", 1, true, 14, 24) { FontCenter = true, ButtonAction = ButtonAction.SwitchPage, ToPage = 2 });
            leftArea.AddChildren(new Button(0, 0x9C5, 0x9C5, 0x9C5, "Video", 1, true, 14, 24) { FontCenter = true, ButtonAction = ButtonAction.SwitchPage, ToPage = 3 });
            leftArea.AddChildren(new Button(0, 0x9C5, 0x9C5, 0x9C5, "Commands", 1, true, 14, 24) { FontCenter = true, ButtonAction = ButtonAction.SwitchPage, ToPage = 4 });
            leftArea.AddChildren(new Button(0, 0x9C5, 0x9C5, 0x9C5, "Tooltip", 1, true, 14, 24) { FontCenter = true, ButtonAction = ButtonAction.SwitchPage, ToPage = 5 });
            leftArea.AddChildren(new Button(0, 0x9C5, 0x9C5, 0x9C5, "General5", 1, true, 14, 24) { FontCenter = true, ButtonAction = ButtonAction.SwitchPage, ToPage = 6 });
            leftArea.AddChildren(new Button(0, 0x9C5, 0x9C5, 0x9C5, "General6", 1, true, 14, 24) { FontCenter = true, ButtonAction = ButtonAction.SwitchPage, ToPage = 7 });

            AddChildren(leftArea);




            int offsetX = 60;
            int offsetY = 60;

            AddChildren(new Button((int)Buttons.Cancel, 0x00F3, 0x00F1, 0x00F2)
            {
                X = 154 + offsetX,
                Y = 405 + offsetY,
                ButtonAction = ButtonAction.Activate,
                ToPage = 0
            });

            AddChildren(new Button((int)Buttons.Apply, 0x00EF, 0x00F0, 0x00EE)
            {
                X = 248 + offsetX,
                Y = 405 + offsetY,
                ButtonAction = ButtonAction.Activate,
                ToPage = 0
            });

            AddChildren(new Button((int)Buttons.Default, 0x00F6, 0x00F4, 0x00F5)
            {
                X = 346 + offsetX,
                Y = 405 + offsetY,
                ButtonAction = ButtonAction.Activate,
                ToPage = 0
            });

            AddChildren(new Button((int)Buttons.Ok, 0x00F9, 0x00F8, 0x00F7)
            {
                X = 443 + offsetX,
                Y = 405 + offsetY,
                ButtonAction = ButtonAction.Activate,
                ToPage = 0
            });

            AcceptMouseInput = false;
            CanMove = true;


            BuildGeneral();
            BuildSounds();
            BuildVideo();
            BuildCommands();

            ChangePage(1);
        }

        public override void Update(double totalMS, double frameMS)
        {

            base.Update(totalMS, frameMS);
        }




        private void BuildGeneral()
        {
            const int PAGE = 1;


            ScrollArea rightArea = new ScrollArea(190, 60, 390, 380, true);



            // FPS
            ScrollAreaItem fpsItem = new ScrollAreaItem();
            Label text = new Label("- FPS:", true, 1);
            fpsItem.AddChildren(text);        
            HSliderBar sliderFPS = new HSliderBar(40, 5, 250, 15, 250, _settings.MaxFPS, HSliderBarStyle.MetalWidgetRecessedBar, true, 1);
            fpsItem.AddChildren(sliderFPS);
            rightArea.AddChildren(fpsItem);


            // Highlight
            Checkbox highlightObjects = new Checkbox(0x00D2, 0x00D3, "Highlight game objects", 1)
            {
                Y = 10, IsChecked = _settings.HighlightGameObjects
            };
            rightArea.AddChildren(highlightObjects);


            // smooth movements
            Checkbox smoothMovement = new Checkbox(0x00D2, 0x00D3, "Smooth movements", 1)
            {
                IsChecked = _settings.SmoothMovement
            };
            rightArea.AddChildren(smoothMovement);


            // preload maps
            Checkbox preloadMaps = new Checkbox(0x00D2, 0x00D3, "Preload maps (it increases the RAM usage)", 1)
            {
                IsChecked = _settings.PreloadMaps
            };
            rightArea.AddChildren(preloadMaps);


            // show % hp mobile
            ScrollAreaItem hpAreaItem = new ScrollAreaItem();
            text = new Label("- Mobiles HP", true, 1)
            {
                Y = 10,
            };
            hpAreaItem.AddChildren(text);
            Checkbox showHPMobile = new Checkbox(0x00D2, 0x00D3, "Show HP", 1)
            {
                X = 25, Y = 30,
                IsChecked = _settings.ShowMobilesHP
            };
            hpAreaItem.AddChildren(showHPMobile);

            int mode = _settings.ShowMobilesHPMode;

            if (mode < 0 || mode > 2)
                mode = 0;

            Combobox hpComboBox = new Combobox(200, 30, 150, new []{ "Percentage", "Line", "Both"}, mode);
            hpAreaItem.AddChildren(hpComboBox);

            rightArea.AddChildren(hpAreaItem);


            // highlight character by flags
            ScrollAreaItem highlightByFlagsItem = new ScrollAreaItem();
            text = new Label("- Mobiles status", true, 1)
            {
                Y = 10,
            };
            highlightByFlagsItem.AddChildren(text);
            Checkbox highlightEnabled = new Checkbox(0x00D2, 0x00D3, "Highlight by state\n(poisoned, yellow hits, paralyzed)", 1)
            {
                X = 25, Y = 30,
                IsChecked = _settings.HighlightMobilesByFlags
            };
            highlightByFlagsItem.AddChildren(highlightEnabled);
            rightArea.AddChildren(highlightByFlagsItem);

            AddChildren(rightArea, PAGE);
        }

        private void BuildSounds()
        {
            const int PAGE = 2;
            ScrollArea rightArea = new ScrollArea(190, 60, 390, 380, true);


            Checkbox soundCheckbox = new Checkbox(0x00D2, 0x00D3, "Sounds", 1)
            {
                IsChecked = _settings.Sound
            };
            rightArea.AddChildren(soundCheckbox);


            ScrollAreaItem item = new ScrollAreaItem();
            Label text = new Label("- Sounds volume:", true, 0, 0, 1);
            HSliderBar sliderVolume = new HSliderBar(40, 5, 180, 0, 255, _settings.SoundVolume, HSliderBarStyle.MetalWidgetRecessedBar, true, 1)
            {
                X = 120
            };
            item.AddChildren(text);
            item.AddChildren(sliderVolume);
            rightArea.AddChildren(item);



            Checkbox musicCheckbox = new Checkbox(0x00D2, 0x00D3, "Music", 1)
            {
                IsChecked = _settings.Music
            };
            rightArea.AddChildren(musicCheckbox);

            item = new ScrollAreaItem();
            text = new Label("- Music volume:", true, 0, 0, 1);
            HSliderBar sliderMusicVolume = new HSliderBar(40, 5, 180, 0, 255, _settings.SoundVolume, HSliderBarStyle.MetalWidgetRecessedBar, true, 1)
            {
                X = 120
            };
            item.AddChildren(text);
            item.AddChildren(sliderMusicVolume);
            rightArea.AddChildren(item);


            item = new ScrollAreaItem();
            Checkbox footstepsCheckbox = new Checkbox(0x00D2, 0x00D3, "Footsteps sound", 1)
            {
                Y = 30,
                IsChecked = _settings.FootstepsSound
            };
            item.AddChildren(footstepsCheckbox);
            rightArea.AddChildren(item);

            Checkbox combatMusicCheckbox = new Checkbox(0x00D2, 0x00D3, "Combat music", 1)
            {
                IsChecked = _settings.CombatMusic
            };
            rightArea.AddChildren(combatMusicCheckbox);

            Checkbox backgroundMusicCheckbox = new Checkbox(0x00D2, 0x00D3, "Reproduce music when ClassicUO is not focussed", 1)
            {
                IsChecked = _settings.BackgroundSound
            };
            rightArea.AddChildren(backgroundMusicCheckbox);

            AddChildren(rightArea, PAGE);
        }

        private void BuildVideo()
        {
            const int PAGE = 3;

        }

        private void BuildCommands()
        {
            const int PAGE = 4;

        }

        private void BuildTooltip()
        {
            const int PAGE = 5;

        }







        public override void Dispose()
        {
            base.Dispose();
        }

        class LeftButton : GumpControl
        {
            public LeftButton()
            {
                CanMove = false;
                CanCloseWithRightClick = false;
                AcceptMouseInput = true;

                //ResizePic background = new ResizePic(0x23F0);
                GumpPicTiled background = new GumpPicTiled(0x462);
                background.AddChildren(new HoveredLabel("General", true, 0xFF, 23, 100, 1, FontStyle.BlackBorder, TEXT_ALIGN_TYPE.TS_CENTER));
                AddChildren(background);

            }
        }


        enum Buttons
        {
            Cancel,
            Apply,
            Default,
            Ok
        }

    }
}
