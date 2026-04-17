using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
namespace MyCalculator;

public partial class Form1 : Form
{
    private double num1;
    private double num2;
    private double result = 0;
    private string operation = "";
    private TextBox txtDisplay;
    private bool isPerformed = false;
    public void equalClick(object? sender, EventArgs e)
    {
        num1 = double.Parse(txtDisplay.Text);
        num2 = double.Parse(txtDisplay.Text);
        switch (operation)
        {
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
            case "*":
                result = num1 * num2;
                break;
            case "/":
                if(num2 != 0)
                {
                    result = num1 / num2;
                }
                else
                {
                    MessageBox.Show("Cannot divide by zero!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result = 0;
                }
                break;
            default:
                MessageBox.Show("Invalid operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;

        }
    }
    public void operatorClick(object? sender, EventArgs e)
    {
        Button btn = (Button)sender!;
        result = double.Parse(txtDisplay.Text); 
        operation = btn.Text;
        isPerformed = true;
    }
    public void numberClick(object? sender, EventArgs e)
    {
        Button btn = (Button)sender!;
        if(txtDisplay.Text == "0" || isPerformed)
        {
            txtDisplay.Clear();
            isPerformed = false;
        }
        txtDisplay.Text += btn.Text;
    }
    public void CreateButton(string text, int x, int y, EventHandler onclick)
    {
        Button btn = new Button
        {
            Text = text,
            Location = new Point(x, y),
            Size = new Size(70, 40),
            Font = new Font("Arial", 14),
        };
        this.Controls.Add(btn);
        btn.Click += onclick;
    }
    public Form1()
    {

        InitializeComponent();
        this.Text = "My Calculator";
        this.Size = new Size(400, 400);
        this.StartPosition = FormStartPosition.CenterScreen;// Khởi tạo vị trí của form

        txtDisplay = new TextBox{
            Location = new Point(20, 20),
            Size = new Size(340, 30),
            Font = new Font("Arial", 16),
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Right,
        };
        this.Controls.Add(txtDisplay);

        for (int i = 0; i < 10; i++)
        {
            int x = 40 + (i % 3) * 80;
            int y = 80 + (i / 3) * 50;
            CreateButton(i.ToString(), x, y, numberClick);

        }
        CreateButton("+", 280, 80, operatorClick);
        CreateButton("-", 280, 130, operatorClick);
        CreateButton("*", 280, 180, operatorClick);
        CreateButton("/", 280, 230, operatorClick);
        CreateButton("=", 280, 280, equalClick);
    }
}
