# Mysql-homework

#### 说明：
数据库大作业，写得很差，没什么逻辑

#### 要求：
超市进销存系统
    在超市的数据库中存储超市的商品信息，超市每天的销售记录，进货记录。
    商品信息包括服装类商品和食品类商品，其中服装类商品中要记录商品的名称，品牌，颜色，大小，适合人群，价格，数量。食品类商品记录商品名称，品牌，保质期截止日期，产地，价格，数量。
    销售记录中包括销售时间，产品编号，数量，单价，总价。
    进货记录中包括进货时间，产品编号，数量，单价，总价。
    该系统能够实现管理员可以根据商品的任意属性的任意组合进行查询实时库存信息和商品明细，加上时间（起始日期和截止日期）查询销售明细。可以根据指定的时间范围统计相应产品的销售信息（ 每种商品或指定商品销售的数量和销售金额）。
    能够实现模拟销售，在销售过程中如果发现某类产品的库存量低于5时，要立刻向采购部门发出采购请求。销售实现的过程尽量使用数据库事物。
    采购部门在收到采购请求是应开始进货，填写进货记录。采购部门每天要统计每种产品的库存量，如果库存量小于10时，要进行采购。
     管理部门每天，每周，每月，每季度，每年统计相应时间范围的销售情况：销售统计（每种产品的销售数量，金额，本时段内的销售总金额，每个品牌的销售总金额），将相应的统计信息存到excel表中或生成pdf文件。
     管理部门可以将某种产品下架，下架的产品信息记录记录在下架产品表中（该表处理产品信息还要有下架时间，下架原因）
    
