
--Prediccion de siguiente orden
WITH CustomerOrders AS (
  SELECT
    O.custid,
    C.companyname AS CustomerName,
    O.orderdate,
    ROW_NUMBER() OVER (PARTITION BY O.custid ORDER BY O.orderdate) AS rn
  FROM Sales.Orders O
  JOIN Sales.Customers C ON O.custid = C.custid
),
OrderDifferences AS (
  SELECT
    curr.custid,
    curr.CustomerName,
    DATEDIFF(DAY, prev.orderdate, curr.orderdate) AS DaysBetween
  FROM CustomerOrders curr
  JOIN CustomerOrders prev
    ON curr.custid = prev.custid
   AND curr.rn = prev.rn + 1
),
Averages AS (
  SELECT
    custid,
    AVG(DaysBetween * 1.0) AS AvgDaysBetween
  FROM OrderDifferences
  GROUP BY custid
),
LastOrders AS (
  SELECT
    O.custid,
    C.companyname AS CustomerName,
    MAX(O.orderdate) AS LastOrderDate
  FROM Sales.Orders O
  JOIN Sales.Customers C ON O.custid = C.custid
  GROUP BY O.custid, C.companyname
)
SELECT
  L.CustomerName,
  L.LastOrderDate,
  DATEADD(DAY, A.AvgDaysBetween, L.LastOrderDate) AS NextPredictedOrder
FROM LastOrders L
LEFT JOIN Averages A ON L.custid = A.custid
ORDER BY L.CustomerName;


--Ordenes de un cliente
SELECT
  orderid,
  requireddate,
  shippeddate,
  shipname,
  shipaddress,
  shipcity
FROM Sales.Orders
WHERE custid = @CustomerId;

--Obtener Empleados
SELECT 
  empid,
  firstname + ' ' + lastname AS FullName
FROM HR.Employees;

--Obtener empresas de reparto
SELECT 
  shipperid,
  companyname
FROM Sales.Shippers;

--Obtener productos
SELECT 
  productid,
  productname
FROM Production.Products;


--Procedimiento para guardar nueva orden (se invocará desde la api) 
CREATE PROCEDURE Sales.usp_AddNewOrderWithDetails
  @CustId         INT,
  @EmpId          INT,
  @ShipperId      INT,
  @ShipName       NVARCHAR(40),
  @ShipAddress    NVARCHAR(60),
  @ShipCity       NVARCHAR(15),
  @ShipRegion     NVARCHAR(15) = NULL,
  @ShipPostalCode NVARCHAR(10) = NULL,
  @ShipCountry    NVARCHAR(15),
  @OrderDate      DATETIME,
  @RequiredDate   DATETIME,
  @ShippedDate    DATETIME = NULL,
  @Freight        MONEY,

  -- OrderDetails
  @ProductId      INT,
  @UnitPrice      MONEY,
  @Qty            SMALLINT,
  @Discount       NUMERIC(4,3),

  -- Output
  @NewOrderId     INT OUTPUT
AS
BEGIN
  SET NOCOUNT ON;

  -- Insertar la orden
  INSERT INTO Sales.Orders (
    custid, empid, shipperid, shipname, shipaddress,
    shipcity, shipregion, shippostalcode, shipcountry,
    orderdate, requireddate, shippeddate, freight
  )
  VALUES (
    @CustId, @EmpId, @ShipperId, @ShipName, @ShipAddress,
    @ShipCity, @ShipRegion, @ShipPostalCode, @ShipCountry,
    @OrderDate, @RequiredDate, @ShippedDate, @Freight
  );

  SET @NewOrderId = SCOPE_IDENTITY();

  -- Insertar detalle del producto
  INSERT INTO Sales.OrderDetails (
    orderid, productid, unitprice, qty, discount
  )
  VALUES (
    @NewOrderId, @ProductId, @UnitPrice, @Qty, @Discount
  );
END;





