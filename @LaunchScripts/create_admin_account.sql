  DELETE FROM [ExamSheet].[dbo].[Accounts]
  WHERE Email like 'admin@admin.com'
  
  INSERT INTO [ExamSheet].[dbo].[Accounts]
  values (newid(), 'admin@admin.com', '3/7xAvX3CyxvJClhmto68gMOTHg=', '/zwsszIQe88YEGSuSDhyHw==', '', '2')