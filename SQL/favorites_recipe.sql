CREATE TABLE favorites_recipe (
    member_id INT,
    favorite_recipe_id INT IDENTITY,
    recipe_id INT NOT NULL,
	primary key(member_id,favorite_recipe_id)
);