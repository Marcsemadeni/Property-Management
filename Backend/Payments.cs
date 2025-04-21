// Ben

public class Payment
{
    public double PaymentAmount { get; set; }
    public DateTime DueDate { get; set; }


    public Payment(double paymentAmount, DateTime dueDate)
    {
        PaymentAmount = paymentAmount;
        DueDate = dueDate;
    }




    public void ProcessPayment()
    {


    }


    public void applyLateFee()
    {


    }


    public void generateReciept()
    {


    }
}
