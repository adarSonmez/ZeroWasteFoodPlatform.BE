namespace Core.Utils.Price;

/// <summary>
/// Helper class for calculating discount rates.
/// </summary>
public static class PriceHelper
{
    /// <summary>
    /// Calculates the discount rate based on the expiration date.
    /// </summary>
    /// <param name="expirationDateTime">The expiration date and time.</param>
    /// <returns>The discount rate.</returns>
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