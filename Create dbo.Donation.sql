/*Select DP.ProductFK, SUM(DP.Quantity), DateOfDonation
From DonationProduct AS DP
INNER JOIN Donation AS D ON DP.DonationFK = D.DonationID
WHERE MONTH(DateOfDonation) = 2 AND YEAR(DateOfDonation) = 2019
Group by DP.ProductFK, DateOfDonation

*/
Select Distinct DateOfDonation
From Donation
Where MONTH(DateOfDonation) = 2 AND YEAR(DateOfDonation) = 2019

Select * From Donation Where DateOfDonation = '2019-02-02 00:00:00.0000000'

Select * From DonationProduct Where DonationFK = 15029
Select * From DonationProduct Where DonationFK = 15033