// Ben

public class Lease
{
    public int leaseTerm{ get; set; }
    public double payment{ get; set; }
    public double deposit{ get; set; }
    public string contract{ get; set; }


    public Lease(int leaseTerm, double payment, double deposit, string contract)
    {
        this.leaseTerm = leaseTerm;
        this.payment = payment;
        this.deposit = deposit;
        this.contract = contract;
    }
}
