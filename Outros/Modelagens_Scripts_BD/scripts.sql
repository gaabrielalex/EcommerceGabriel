
CREATE TABLE produto (
	id_produto INT NOT NULL IDENTITY(1, 1),
	descricao VARCHAR(100) NOT NULL UNIQUE,
	preco_unitario NUMERIC(7, 2) NOT NULL CHECK (preco_unitario > 0),
	saldo_estoque INT NOT NULL CHECK (saldo_estoque >= 0),
	PRIMARY KEY(id_produto)
);

CREATE TABLE venda (
	id_venda INT NOT NULL IDENTITY(1, 1), 
	data_venda DATE NOT NULL DEFAULT FORMAT(getdate(), 'DD/MM/YYYY'),
	nome_cliente varchar(100) NOT NULL,
	vlr_total numeric(10,2) NOT NULL CHECK (vlr_total > 0),
	PRIMARY KEY(id_venda)
);

CREATE TABLE item_venda (
	id_venda INT NOT NULL,
	id_produto INT NOT NULL,
	qtde INT NOT NULL CHECK (qtde > 0),
	preco_unitario NUMERIC(7,2) NOT NULL,
	vlr_total_item NUMERIC(8,2) NOT NULL CHECK (vlr_total_item > 0),
	PRIMARY KEY(id_venda, id_produto),
	FOREIGN KEY(id_venda) REFERENCES venda(id_venda) ON DELETE CASCADE,
	FOREIGN KEY(id_produto) REFERENCES produto(id_produto)
);