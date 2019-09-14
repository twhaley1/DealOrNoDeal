using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DealOrNoDeal.View
{

    /// <summary>
    ///     Custom ContentDialog that allows the user to select how many rounds they wish to play.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class RoundSelectionDialog
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoundSelectionDialog"/> class.
        /// </summary>
        public RoundSelectionDialog()
        {
            this.InitializeComponent();
        }


        /// <summary>
        ///     7 rounds.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        /// <summary>
        ///     13 rounds.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        /// <summary>
        ///     10 rounds.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private void RoundSelectionDialog_OnCloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        { 
        }
    }
}
