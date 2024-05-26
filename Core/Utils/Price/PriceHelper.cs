namespace Core.Utils.Price;

public static class PriceHelper
{
    /// <summary>
    ///     Gradually increase percentage of discount, so that on the expiration date the discount is 90%.
    ///     Starting discount rate is 20% which is applied 20+ days before the expiration date.
    /// </summary>
    /// <param name="expirationDateTime"></param>
    /// <returns>Discount rate</returns>
    public static int CalculateDiscountRate(DateTime expirationDateTime)
    {
        var today = DateTime.Today;
        var expirationDate = expirationDateTime.Date;

        var daysUntilExpiration = (expirationDate - today).TotalDays;

        return daysUntilExpiration switch
        {
            < 0 => -1,
            0 => 90,
            > 20 => 20,
            _ => 20 + (int)(70 * (1 - daysUntilExpiration / 20))
        };
    }
}