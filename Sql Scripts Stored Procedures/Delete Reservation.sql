USE [AirBB]
GO
/****** Object:  StoredProcedure [dbo].[DeleteReservation]    Script Date: 3/9/2024 9:58:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[DeleteReservation]
	-- Add the parameters for the stored procedure here
	@ReservationId bigint
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- get room id
	Declare @RoomId bigint = (
	Select top  1 mr.Id
	From MyReservation mres
	Join MyRoom mr on mres.RoomId = mr.Id
	)
	Begin Try
	Begin Tran
		Delete From MyReservation
		Where Id = @ReservationId

		Update mr
		Set mr.BeingUsed = 0
		From MyRoom mr
		Where mr.Id = @RoomId

		Commit
		Select 'Reservation Deleted Successfully'[Message], @ReservationId[ReservationId], @RoomId[RoomId]
	End Try
	Begin Catch
		rollback
		Select 'Error Deleting Reservation'[Message], @ReservationId[ReservationId], @RoomId[RoomId]
	End Catch
    -- Insert statements for procedure here
	
END
