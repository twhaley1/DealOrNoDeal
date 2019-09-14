using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using DealOrNoDeal.Model;
using DealOrNoDeal.Util;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DealOrNoDeal.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DealOrNoDealPage
    {
        #region Data members

        /// <summary>
        ///     The application height
        /// </summary>
        public const int ApplicationHeight = 650;

        /// <summary>
        ///     The application width
        /// </summary>
        public const int ApplicationWidth = 500;

        private IList<Button> briefcaseButtons;
        private IList<Border> dollarAmountLabels;

        private readonly GameManager gameManager;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DealOrNoDealPage" /> class.
        /// </summary>
        public DealOrNoDealPage()
        {
            this.InitializeComponent();
            this.initializeUiDataAndControls();
            this.gameManager = new GameManager();

            this.casesToOpenLabel.Text = StringConstants.SetUpTheGame;
            this.disableDealButtons();
            this.disableAllBriefcaseButtons();
        }

        #endregion

        #region Methods

        private async void ConfigureGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            var gameModeSelector = new GameModeDialog();
            var result = await gameModeSelector.ShowAsync();

            this.adjustGreetingBasedOnGameMode(result);
            this.gameManager.SetGameMode(result);
            this.changeDollarLabels(this.gameManager.GetAllDollarAmounts());
            
            //TODO: Add functionality to specify number of rounds.

            this.disableSetUpButton();
            this.enableAllVisibleButtons();
        }

        private void adjustGreetingBasedOnGameMode(ContentDialogResult result)
        {
            switch (result)
            {
                case ContentDialogResult.Primary:
                    this.roundLabel.Text = StringConstants.MegaGameModeWelcome;
                    this.casesToOpenLabel.Text = StringConstants.SelectYourCase;
                    break;
                case ContentDialogResult.Secondary:
                    this.roundLabel.Text = StringConstants.SyndicateGameModeWelcome;
                    this.casesToOpenLabel.Text = StringConstants.SelectYourCase;
                    break;
                case ContentDialogResult.None:
                    this.roundLabel.Text = StringConstants.RegularGameModeWelcome;
                    this.casesToOpenLabel.Text = StringConstants.SelectYourCase;
                    break;
                default:
                    this.roundLabel.Text = string.Empty;
                    break;
            }
        }

        private void changeDollarLabels(IList<int> dollarAmounts)
        {
            foreach (var border in this.dollarAmountLabels)
            {
                ((TextBlock) border.Child).Text = dollarAmounts[this.dollarAmountLabels.IndexOf(border)].ToString("C").Replace(".00", string.Empty);
            }
        }

        private void initializeUiDataAndControls()
        {
            this.setPageSize();

            this.briefcaseButtons = new List<Button>();
            this.dollarAmountLabels = new List<Border>();
            this.buildBriefcaseButtonCollection();
            this.buildDollarAmountLabelCollection();

            this.disableDealButtons();
        }

        private void setPageSize()
        {
            ApplicationView.PreferredLaunchViewSize = new Size {Width = ApplicationWidth, Height = ApplicationHeight};
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(ApplicationWidth, ApplicationHeight));
        }

        private void buildDollarAmountLabelCollection()
        {
            this.dollarAmountLabels.Clear();

            this.dollarAmountLabels.Add(this.label0Border);
            this.dollarAmountLabels.Add(this.label1Border);
            this.dollarAmountLabels.Add(this.label2Border);
            this.dollarAmountLabels.Add(this.label3Border);
            this.dollarAmountLabels.Add(this.label4Border);
            this.dollarAmountLabels.Add(this.label5Border);
            this.dollarAmountLabels.Add(this.label6Border);
            this.dollarAmountLabels.Add(this.label7Border);
            this.dollarAmountLabels.Add(this.label8Border);
            this.dollarAmountLabels.Add(this.label9Border);
            this.dollarAmountLabels.Add(this.label10Border);
            this.dollarAmountLabels.Add(this.label11Border);
            this.dollarAmountLabels.Add(this.label12Border);
            this.dollarAmountLabels.Add(this.label13Border);
            this.dollarAmountLabels.Add(this.label14Border);
            this.dollarAmountLabels.Add(this.label15Border);
            this.dollarAmountLabels.Add(this.label16Border);
            this.dollarAmountLabels.Add(this.label17Border);
            this.dollarAmountLabels.Add(this.label18Border);
            this.dollarAmountLabels.Add(this.label19Border);
            this.dollarAmountLabels.Add(this.label20Border);
            this.dollarAmountLabels.Add(this.label21Border);
            this.dollarAmountLabels.Add(this.label22Border);
            this.dollarAmountLabels.Add(this.label23Border);
            this.dollarAmountLabels.Add(this.label24Border);
            this.dollarAmountLabels.Add(this.label25Border);
        }

        private void buildBriefcaseButtonCollection()
        {
            this.briefcaseButtons.Clear();

            this.briefcaseButtons.Add(this.case0);
            this.briefcaseButtons.Add(this.case1);
            this.briefcaseButtons.Add(this.case2);
            this.briefcaseButtons.Add(this.case3);
            this.briefcaseButtons.Add(this.case4);
            this.briefcaseButtons.Add(this.case5);
            this.briefcaseButtons.Add(this.case6);
            this.briefcaseButtons.Add(this.case7);
            this.briefcaseButtons.Add(this.case8);
            this.briefcaseButtons.Add(this.case9);
            this.briefcaseButtons.Add(this.case10);
            this.briefcaseButtons.Add(this.case11);
            this.briefcaseButtons.Add(this.case12);
            this.briefcaseButtons.Add(this.case13);
            this.briefcaseButtons.Add(this.case14);
            this.briefcaseButtons.Add(this.case15);
            this.briefcaseButtons.Add(this.case16);
            this.briefcaseButtons.Add(this.case17);
            this.briefcaseButtons.Add(this.case18);
            this.briefcaseButtons.Add(this.case19);
            this.briefcaseButtons.Add(this.case20);
            this.briefcaseButtons.Add(this.case21);
            this.briefcaseButtons.Add(this.case22);
            this.briefcaseButtons.Add(this.case23);
            this.briefcaseButtons.Add(this.case24);
            this.briefcaseButtons.Add(this.case25);

            this.storeBriefCaseIndexInControlsTagProperty();
        }

        private void storeBriefCaseIndexInControlsTagProperty()
        {
            for (var i = 0; i < this.briefcaseButtons.Count; i++)
            {
                this.briefcaseButtons[i].Tag = i;
            }
        }

        private void briefcase_Click(object sender, RoutedEventArgs e)
        {
            var clickedBriefcaseButton = (Button) sender;

            var clickedBriefcaseId = this.getBriefcaseID(clickedBriefcaseButton);

            var dollarAmountInClickedBriefcase = this.gameManager.GetIdsDollarAmount(clickedBriefcaseId);
            if (this.gameManager.IsGameStarted)
            {
                this.findAndGrayOutGameDollarLabel(dollarAmountInClickedBriefcase);
            }

            if (this.gameManager.GetIsFinalRound)
            {
                this.onGameEndingCaseSelection(clickedBriefcaseButton);
            }
            else
            {
                this.gameManager.ProcessBriefCaseRemoval(clickedBriefcaseId);
                this.modifyUiComponentsBriefcaseClick(clickedBriefcaseButton);
            }
        }

        private void onGameEndingCaseSelection(Button selectedButton)
        {
            this.summaryOutput.Text = "Congrats you win " +
                                      this.gameManager.GetIdsDollarAmount(this.getBriefcaseID(selectedButton)).ToString("C") + "\n" +
                                      "GAME OVER";
            this.disableAllBriefcaseButtons();
            this.collapseAllBriefcaseButtons();
            this.gameManager.EndGame();
        }

        private int getBriefcaseID(Button selectedBriefCase)
        {
            return (int) selectedBriefCase.Tag;
        }

        private void modifyUiComponentsBriefcaseClick(Button clickedBriefcaseButton)
        {
            this.removeBriefcaseButton(clickedBriefcaseButton);

            this.updateLabelInformation();
            if (this.gameManager.GetNoRemainingCasesLeft)
            {
                this.disableAllBriefcaseButtons();
                this.enableDealButtons();
                this.updateSummaryOutput();
            }
        }

        private void removeBriefcaseButton(Button clickedButton)
        {
            clickedButton.IsEnabled = false;
            clickedButton.Visibility = Visibility.Collapsed;
        }

        private void findAndGrayOutGameDollarLabel(int amount)
        {
            foreach (var currentDollarAmount in this.dollarAmountLabels)
            {
                if (grayOutLabelIfMatchesDollarAmount(amount, currentDollarAmount))
                {
                    break;
                }
            }
        }

        private static bool grayOutLabelIfMatchesDollarAmount(int amount, Border currentDollarAmountLabel)
        {
            var matched = false;

            if (currentDollarAmountLabel.Child is TextBlock dollarTextBlock)
            {
                var labelAmount = int.Parse(dollarTextBlock.Text, NumberStyles.Currency);
                if (labelAmount == amount)
                {
                    currentDollarAmountLabel.Background = new SolidColorBrush(Colors.Gray);
                    matched = true;
                }
            }

            return matched;
        }

        private void updateLabelInformation()
        {
            this.updateRoundLabel();
            this.updateCasesToOpenLabel();
            this.updateSummaryOutput();
        }

        private void updateRoundLabel()
        {
            if (this.gameManager.GetIsFinalRound)
            {
                this.roundLabel.Text = "This is the final round";
            }
            else
            {
                this.roundLabel.Text = "Round " + this.gameManager.GetCurrentRound + ": " +
                                       EnglishStringUtility.AppendSDependingOnNumber(
                                           this.gameManager.GetNumberCasesToOpenInCurrentRound, " cases") +
                                       " to open.";
            }
        }

        private void updateCasesToOpenLabel()
        {
            if (this.gameManager.GetIsFinalRound)
            {
                this.casesToOpenLabel.Text = "Select final case above.";
            }
            else
            {
                this.casesToOpenLabel.Text =
                    EnglishStringUtility.AppendSDependingOnNumber(this.gameManager.GetNumberCasesLeftInCurrentRound,
                        " more cases") +
                    " to open.";
            }
        }

        private void updateSummaryOutput()
        {
            if (this.gameManager.GetNoRemainingCasesLeft)
            {
                this.summaryOutput.Text = this.buildMinMaxOfferString() + "\n" +
                                          "Current offer: " + this.gameManager.GetCurrentOffer.ToString("C") + "\n" +
                                          "Deal or No Deal?";
            }
            else if (this.gameManager.GetIsFirstRound)
            {
                this.summaryOutput.Text = "Your case: " + (this.gameManager.GetStartingBriefcaseId + 1);
            }
            else if (this.gameManager.GetIsFinalRound)
            {
                if (!this.gameManager.IsGameOver)
                {
                    this.summaryOutput.Text = this.buildMinMaxOfferString();
                }
            }
        }

        private string buildMinMaxOfferString()
        {
            return "Offers: Max: " + this.gameManager.GetMaxOffer.ToString("C") + "; Min: " + this.gameManager.GetMinOffer.ToString("C") + "\n" +
                "Average: " + this.gameManager.GetAvgOffer.ToString("C");
        }

        private void dealButton_Click(object sender, RoutedEventArgs e)
        {
            this.updateDealSummaryOutput();

            this.disableDealButtons();
            this.collapseAllBriefcaseButtons();
        }

        private void updateDealSummaryOutput()
        {
            this.summaryOutput.Text = "Your case contained: " + this.gameManager.GetStartingBriefcaseDollarAmount.ToString("C") + "\n" +
                                      "Accepted offer: " + this.gameManager.GetCurrentOffer.ToString("C") + "\n" +
                                      "GAME OVER";
        }

        private void noDealButton_Click(object sender, RoutedEventArgs e)
        {
            this.modifyUiComponentsNoDealClick();
            this.updateGameStatePostDeal();
            this.updateLabelInformation();
        }

        private void modifyUiComponentsNoDealClick()
        {
            this.updateNoDealSummaryOutput();

            if (this.gameManager.GetIsSemiFinalRound)
            {
                this.showPlayersStartingBriefcaseButton();
                this.moveFinalButtonsToMiddleRow();
                this.gameManager.ReintroduceStartingCase();
            }

            this.enableAllVisibleButtons();
            this.disableDealButtons();
        }

        private void moveFinalButtonsToMiddleRow()
        {
            var startingId = this.gameManager.GetStartingBriefcaseId;
            var startingButton = this.briefcaseButtons.Single(button => (int) button.Tag == startingId);
            var lastCaseId = this.gameManager.GetLastBriefcasesId();
            var lastButton = this.briefcaseButtons.Single(button => (int) button.Tag == lastCaseId);

            var startingButtonParent = (Panel) startingButton.Parent;
            startingButtonParent.Children.Remove(startingButton);
            var lastButtonParent = (Panel) lastButton.Parent;
            lastButtonParent.Children.Remove(lastButton);

            var finalTwoButtons = new List<Button> {startingButton, lastButton}.OrderBy(button => (int) button.Tag).ToList();
            finalTwoButtons.ForEach(button => this.middleStackPanel.Children.Add(button));
        }

        private void updateGameStatePostDeal()
        {
            this.gameManager.NextRound();
        }

        private void updateNoDealSummaryOutput()
        {
            this.summaryOutput.Text = this.buildMinMaxOfferString() + "\n" +
                                          "Last offer: " + this.gameManager.GetCurrentOffer.ToString("C");
        }

        private void showPlayersStartingBriefcaseButton()
        {
            var startingId = this.gameManager.GetStartingBriefcaseId;
            var startingButton = this.briefcaseButtons.Single(button => (int) button.Tag == startingId);

            startingButton.IsEnabled = true;
            startingButton.Visibility = Visibility.Visible;
        }

        private void enableDealButtons()
        {
            this.dealButton.IsEnabled = true;
            this.noDealButton.IsEnabled = true;
            this.dealButton.Visibility = Visibility.Visible;
            this.noDealButton.Visibility = Visibility.Visible;
        }

        private void disableDealButtons()
        {
            this.dealButton.IsEnabled = false;
            this.noDealButton.IsEnabled = false;
            this.dealButton.Visibility = Visibility.Collapsed;
            this.noDealButton.Visibility = Visibility.Collapsed;
        }

        private void disableSetUpButton()
        {
            this.configureGameButton.IsEnabled = false;
            this.configureGameButton.Visibility = Visibility.Collapsed;
        }

        private void disableAllBriefcaseButtons()
        {
            foreach (var button in this.briefcaseButtons)
            {
                button.IsEnabled = false;
            }
        }

        private void collapseAllBriefcaseButtons()
        {
            foreach (var button in this.briefcaseButtons)
            {
                button.Visibility = Visibility.Collapsed;
            }
        }

        private void enableAllVisibleButtons()
        {
            foreach (var button in this.briefcaseButtons)
            {
                if (button.Visibility == Visibility.Visible)
                {
                    button.IsEnabled = true;
                }
            }
        }

        #endregion
    }
}