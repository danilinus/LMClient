using System.Windows.Controls;
using System.Windows.Media;

namespace Messages
{
	public partial class SmmS : UserControl
	{
		public Color BackColor = Color.FromArgb(255, 244, 244, 245),
					 NameColor = Color.FromArgb(255, 255, 255, 255),
					 TextColor = Color.FromArgb(255, 255, 255, 255);

		public SmmS() => InitializeComponent();

		public SmmS(string ContentText, string NameUser)
		{
			txtText.Text = ContentText;
			txtName.Content = NameUser;
			InitializeComponent();
		}

		public void GetBackColor(Color BackColor)
		{
			this.BackColor = BackColor;
			Griding.Background = new SolidColorBrush(this.BackColor);
		}

		public void GetContentText(string Text) => txtText.Text = Text;

		public void GetContentName(string Text) => txtName.Content = Text;

		public void GetNameColor(Color NameColor)
		{
			this.NameColor = NameColor;
			txtName.Foreground = new SolidColorBrush(this.NameColor);
		}

		public void GetTextColor(Color TextColor)
		{
			this.TextColor = TextColor;
			txtText.Foreground = new SolidColorBrush(TextColor);
		}
	}
}