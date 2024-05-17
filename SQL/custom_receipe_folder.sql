CREATE TABLE custom_receipe_folder (
    member_id INT,
    custom_folder_id INT IDENTITY,
    custom_folder_name NVARCHAR(50) NOT NULL,
    recipe_id INT NOT NULL,
	primary key(member_id,custom_folder_id)
);