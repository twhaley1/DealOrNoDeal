using System.Collections.Generic;
using System.Globalization;
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
        public const int ApplicationHeight = 500;

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
        ///     Initializes a new instance of the <see cref="DealOrNoDealPage"/> class.
        /// </summary>
        public DealOrNoDealPage()
        {
            this.InitializeComponent();
            this.initializeUiDataAndControls();
            this.gameManager = new GameManager();
        }

        #endregion

        #region Methods

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

            var dollarAmountInClickedBriefcase = this.gameManager.CaseManager.GetDollarAmountIn(clickedBriefcaseId);
            if (this.gameManager.IsGameStarted)
            {
                this.findAndGrayOutGameDollarLabel(dollarAmountInClickedBriefcase);
            }

            this.gameManager.ProcessBriefCaseRemoval(clickedBriefcaseId);

            this.modifyUiComponentsBriefcaseClick(clickedBriefcaseButton);
        }

        private int getBriefcaseID(Button selectedBriefCase)
        {
            return (int)selectedBriefCase.Tag;
        }

        private void modifyUiComponentsBriefcaseClick(Button clickedBriefcaseButton)
        {
            this.removeBriefcaseButton(clickedBriefcaseButton);

            this.updateLabelInformation();
            if (this.gameManager.NoRemainingCasesLeft)
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
            if (this.gameManager.RoundManager.IsFinalRound)
            {
                this.roundLabel.Text = "This is the final round";
            }
            else
            {
                this.roundLabel.Text = "Round " + this.gameManager.RoundManager.CurrentRound + ": " +
                    EnglishStringUtility.AppendSDependingOnNumber(this.gameManager.RoundManager.GetNumberOfCasesToOpenThisRound(), " cases") +
                    " to open.";
            }
        }

        private void updateCasesToOpenLabel()
        {
            if (this.gameManager.RoundManager.IsFinalRound)
            {
                this.casesToOpenLabel.Text = "Select a case below.";
            }
            else
            {
                this.casesToOpenLabel.Text =
                    EnglishStringUtility.AppendSDependingOnNumber(this.gameManager.CasesLeftInCurrentRound, " more cases") +
                    " to open.";
            }
        }

        private void updateSummaryOutput()
        {
            if (this.gameManager.NoRemainingCasesLeft)
            {
                this.summaryOutput.Text = "Offers: Min: " + this.gameManager.Banker.MinOffer.ToString("C") +
                                          "; Max: " + this.gameManager.Banker.MaxOffer.ToString("C") + "\n" +
                                          "Current offer: " + this.gameManager.Banker.CurrentOffer.ToString("C") + "\n" +
                                          "Deal or No Deal?";
            }
            else if (this.gameManager.RoundManager.IsFirstRound)
            {
                this.summaryOutput.Text = "Your case: " + (this.gameManager.CaseManager.StartingCaseId + 1);
            }
            else if (this.gameManager.RoundManager.IsFinalRound)
            {
                this.summaryOutput.Text = "Offers: Min: " + this.gameManager.Banker.MinOffer.ToString("C") + "; " +
                                          "Max: " + this.gameManager.Banker.MaxOffer.ToString("C");
            }
        }

        private void dealButton_Click(object sender, RoutedEventArgs e)
        {
            this.updateDealSummaryOutput();

            this.disableDealButtons();
            this.collapseAllBriefcaseButtons();
        }

        private void updateDealSummaryOutput()
        {
            if (this.gameManager.RoundManager.IsFinalRound)
            {
                this.summaryOutput.Text =
                    "Congrats you win " + this.gameManager.CaseManager.StartingCase.DollarAmount.ToString("C") + "\n" +
                    "GAME OVER";
            }
            else
            {
                this.summaryOutput.Text = "Your case contained: " + this.gameManager.CaseManager.StartingCase.DollarAmount.ToString("C") + "\n" +
                                          "Accepted offer: " + this.gameManager.Banker.CurrentOffer.ToString("C") + "\n" +
                                          "GAME OVER";
            }
        }

        private void noDealButton_Click(object sender, RoutedEventArgs e)
        {
            this.modifyUiComponentsNoDealClick();
            this.updateGameStatePostDeal();
            this.modifyLabelNoDealClick();
        }

        private void modifyUiComponentsNoDealClick()
        {
            this.updateNoDealSummaryOutput();

            if (this.gameManager.RoundManager.IsSemiFinalRound)
            {
                this.collapseAllBriefcaseButtons();
                this.changeDealButtonsToFinalBriefcaseButtons();
            }
            else if (this.gameManager.RoundManager.IsFinalRound)
            {
                this.disableDealButtons();
            }
            else
            {
                this.disableDealButtons();
                this.enableAllVisibleButtons();
            }
        }

        private void modifyLabelNoDealClick()
        {
            this.updateLabelInformation();
        }

        private void updateGameStatePostDeal()
        {
            this.gameManager.NextRound();
        }

        private void updateNoDealSummaryOutput()
        {
            if (this.gameManager.RoundManager.IsFinalRound)
            {
                this.summaryOutput.Text = "Congrats you win " + this.gameManager.CaseManager.GetLastBriefcase().DollarAmount.ToString("C") + "\n" +
                                          "GAME OVER";
            }
            else
            {
                this.summaryOutput.Text = "Offers: Min: " + this.gameManager.Banker.MinOffer.ToString("C") +
                                          "; Max: " + this.gameManager.Banker.MaxOffer.ToString("C") +
                                          "\n" +
                                          "Last offer: " + this.gameManager.Banker.CurrentOffer.ToString("C");
            }
        }

        private void changeDealButtonsToFinalBriefcaseButtons()
        {
            this.dealButton.Content = "Case " + (this.gameManager.CaseManager.StartingCaseId + 1);
            this.noDealButton.Content = "Case " + (this.gameManager.CaseManager.GetLastBriefcase().Id + 1);
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