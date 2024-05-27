ALTER TABLE userInfo
ALTER COLUMN name nvarchar(20) not null;

ALTER TABLE userInfo
ALTER COLUMN email NVARCHAR(20) NOT NULL;
ALTER TABLE userInfo
ADD CONSTRAINT UQ_email UNIQUE (email);

ALTER TABLE userInfo
ALTER COLUMN password NVARCHAR(20) NOT NULL;
ALTER TABLE userInfo
ALTER COLUMN userPermissions int not null ;


ALTER TABLE userInfo
ALTER COLUMN createTime SMALLDATETIME NOT NULL;
ALTER TABLE userInfo
ADD CONSTRAINT DF_createTime DEFAULT GETDATE() FOR createTime;
  
  UPDATE userInfo
SET createTime = GETDATE()
WHERE createTime IS NULL;






--�ݬ����O�_�s�b
SELECT * 
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
WHERE TABLE_NAME = 'userInfo' AND CONSTRAINT_TYPE = 'UNIQUE';