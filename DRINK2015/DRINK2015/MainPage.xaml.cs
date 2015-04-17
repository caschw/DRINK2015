using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DRINK2015.ViewModels;
using Windows.Storage;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DRINK2015.Domain;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace DRINK2015
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string filename = "DRINK2015PartyList.json";
        private PartyConfiguration _partyConfig;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            _partyConfig = new PartyConfiguration();
            _partyConfig.PartyListUpdated += OnPartyListUpdated;
            Task.Run(async() => { await Loaded(); });
            _partyConfig.GetUpdatedPartyList();
        }

        private async Task Loaded()
        {
            var file = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);
            var read = await FileIO.ReadTextAsync(file);
            var items = JsonConvert.DeserializeObject<List<PartyDetail>>(read);
            if (items == null || items.Count == 0)
            {
               items = await CreateBaseFile();
            }
            DataContext = new MainPageViewModel { PartyItems = items };
        }

        private async Task<List<PartyDetail>> CreateBaseFile()
        {
            var partyList = new List<PartyDetail>();
            partyList.Add(new PartyDetail
            {
                Name = "Telerik //BUILD party",
                Description = "Telerik will have two booths at the //BUILD expo center--one in the Windows pavilion and one in Office 365. Sure with Telerik DevCraft, the Windows developer is equipped to build any app for any platform; but we are really excited about an upcoming announcement around Office 365 development and Telerik Kendo UI. Keep a lookout! We’ll also be delivering two Express Talks that are located on the 1st floor of Moscone West talking about the latest improvements in our products.\nHow do you find your way into the esteemed club? No, you don’t have to own an island, just come chat with us at the Telerik booths, where you can get a special wrist band that gets you access.  Want a shortcut? Tweet and grab any of us four Telerikers somewhere at //BUILD for a pass: @samidip, @burkeholland, @mbcrump and @mehfuzh. Handsome faces, right? Fine, we have muscular fingers from all the typing and sport big smiles. Just find us!",
                Start = new DateTime(2015, 4, 29, 20, 30, 0),
                End = new DateTime(2015, 4, 29, 10, 22, 0),
                Address = "Thirsty Bear Brewing Company 661 Howard Street, San Francisco, CA 94105",
                RequiresInvite = true,
                Sponsors = "Telerik"
            });
            partyList.Add(new PartyDetail
            {
                Name = "MVP Happy Hour(s)",
                Description = "Infragistics and the MVP Award Program are excited to invite you to network, drink and eat with us in a great atmosphere as provided by Thirsty Bear Brewing Co. This bar is one block away from the conference center, so you'll be quenching your thirst from the short brisk walk in 2 minutes or less! There will be contests, give-aways, and lots of great company, tapas, organic beer and other awesome things to consume!\nWe've adjusted the time so that you can go to other parties that are hosted the same evening.\nWe're so happy you are attending BUILD and even happier about the work you do every day in technical communities. Join us!\nInfragistics and MVP Award Program",
                Start = new DateTime(2015, 4, 29, 18, 30, 0),
                End = new DateTime(2015, 4, 29, 20, 30, 0),
                Address = "Thirsty Bear Brewing Company 661 Howard Street, San Francisco, CA 94105",
                RequiresInvite = true,
                Sponsors = "Infragistics, MVP Award Program"
            });
            await _partyConfig.SavePartyList(partyList);
            return partyList;
        }

        private void OnPartyListUpdated(PartyListEventArgs e)
        {
            DataContext = new MainPageViewModel { PartyItems = e.PartyList };
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }
    }
}
