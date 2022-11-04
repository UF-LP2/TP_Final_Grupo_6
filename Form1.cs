using csvfiles;
namespace tp_final;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        var csv_ = new csvfiles._csv();
        csv_.read_csv();
    }
}
