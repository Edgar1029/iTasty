CREATE TABLE shopping_receipe (
    member_id INT,
    shopping_receipe_id INT IDENTITY,
    recipe_id INT NOT NULL,
	primary key(member_id,shopping_receipe_id)
);