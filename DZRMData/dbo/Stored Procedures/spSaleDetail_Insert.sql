CREATE PROCEDURE [dbo].[spSaleDetail_Insert]
	@SaleId int,
	@ProductId int,
	@Quantity int,
	@PurchasedPrice money,
	@Tax money
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.[SaleDetail'](SaleId, ProductId, Quantity, PurchasedPrice, Tax)
	VALUES (@SaleId, @ProductId, @Quantity, @PurchasedPrice, @Tax);
END
