CREATE TABLE edited_recipe(
    member_id INT,
    edited_recipe_id INT IDENTITY,
    recipe_id INT NOT NULL,
	primary key(member_id,edited_recipe_id)
);
