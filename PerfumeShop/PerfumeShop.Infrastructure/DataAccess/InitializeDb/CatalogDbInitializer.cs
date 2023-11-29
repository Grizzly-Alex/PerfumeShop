namespace PerfumeShop.Infrastructure.DataAccess.InitializeDb;

public sealed class CatalogDbInitializer : IDbInitializer
{
	private readonly CatalogDbContext _dbContext;

	public CatalogDbInitializer(CatalogDbContext dbContext)
	{
		_dbContext = dbContext;
	}


	public async Task Initialize()
	{
		if (_dbContext.Database.IsSqlServer() && _dbContext.Database.GetPendingMigrations().Any())
		{
			await _dbContext.Database.MigrateAsync();
		}

		if (!await _dbContext.AromaTypes.AnyAsync())
		{
			await _dbContext.AromaTypes.AddRangeAsync(GetAromaTypes());
			await _dbContext.SaveChangesAsync();
		}

		if (!await _dbContext.Brands.AnyAsync())
		{
			await _dbContext.Brands.AddRangeAsync(GetBrands());
			await _dbContext.SaveChangesAsync();
		}

		if (!await _dbContext.ReleaseForms.AnyAsync())
		{
			await _dbContext.ReleaseForms.AddRangeAsync(GetReleaseForms());
			await _dbContext.SaveChangesAsync();
		}

		if (!await _dbContext.Products.AnyAsync())
		{
			await _dbContext.Products.AddRangeAsync(GetProducts());
			await _dbContext.SaveChangesAsync();
		}		
	}

	private IEnumerable<CatalogProduct> GetProducts()
	{
		return new List<CatalogProduct>
		{
			new(DateTime.UtcNow, 2, 1, 1, 2, "Purpose", 85.00m, 10, 100, "31e31ce4-2150-4c86-aa3a-adb83fdde4a8.png",
			"<p>Energetic bergamot runs in an icy draft between trunks densely covered with moss. Cold pink pepper" +
			" pleasantly tickles the nose, and incense sets you up for a new spiritual discovery.<br>Smoky, thunderous" +
			" vetiver is combined with tobacco accents of rose and earthy papyrus. Silky sandalwood and akigalawood wrap " +
			"the composition in a pleasantly moist mist. Purpose is the destination, the end of the road and the place where" +
			" the sun is always at its zenith and the aldehyde rays of mystical softly shine through the saffron-suede clouds</p>", 59.99m),

			new(DateTime.UtcNow, 3, 1, 2, 2, "Ego Stratis", 65.00m, 3, 100, "679b7b19-968d-484c-9efa-00129a46f10b.png",
			"<p>The Ego Stratis girl is open and boundless, like the ocean. Her angels and demons chat peacefully over tea," +
			" her bold nature and vulnerability tease each other like old friends.<br>Ego Stratis has no boundaries, and opposites" +
			" are parts of a single whole. The fragrance is both masculine and feminine, bright and calm, passionate and platonic." +
			" The softness of blueberries liberates the brave &ldquo;eau de cologne&rdquo; heart, delicate musk is combined with " +
			"temperamental cedar, citrus sparks scatter in the depths of the sea.3</p>"),

			new(DateTime.UtcNow, 3, 3, 3, 2, "Magnolia Bliss", 67.00m, 5, 100, "8335959a-e5d8-4203-83d1-b21f5a96ae67.png", 
			"<p>Magnolia Bliss is a light breath of freedom, another charming Juliet, placed in the socio-cultural context" +
			" of the summer of 1969. The scent of magnolia is in the air, dancing in her long, tousled hair. Barefoot, flower" +
			" crowned muse Janis Joplin feasts on a juicy plum.<br>Time seemed to freeze in this moment: here is eternal summer," +
			" hippie music, Polaroid pictures, the mood of resolute peacefulness. Juliet embodies the power of flowers, there is" +
			" a bohemian spirit in her, but at the same time her rebellious nature does not fade for a moment. The fragrance is composed" +
			" of magnolia essence, juicy but not sweet mirabelle plum and fresh bergamot essence. The signature amber-musky base completes" +
			" the composition.</p>"),

			new(DateTime.UtcNow, 3, 3, 3, 2, "Lili Fantasy", 87.00m, 12, 100, "81d0f5aa-f2d9-4f3a-a9ab-552c2fbd3198.png",
			"<p>Lili Fantasy is an extravagant cocktail of white flowers and ambery notes with a playful hint of gum. A bright perfume" +
			" whirlwind will herald your appearance... and will remain in memory for a long time after your disappearance.</p>"),

			new(DateTime.UtcNow, 3, 3, 3, 2, "Lipstick Fever", 87.00m, 12, 100, "ff8334df-9423-4979-a93a-053cb4f2d932.png",
			"<p>The composition of Lipstick Fever contains notes that have historically been used in lipstick: iris, violet absolute and" +
			" raspberry. Woody shades of patchouli and cedar wood and reproduce the scent of a leather handbag.</p>\r\n<p>Lipstick Fever is" +
			" a modern gourmand composition with an expressive feminine character.</p>", 69.99m),

			new(DateTime.UtcNow, 5, 1, 4, 1, "Sale Gosse", 120.00m, 3, 100, "728a479d-ae36-420f-9788-0936b98917e8.png","" +
			"<p>Aroma-fidget Sale Gosse plays with notes of petitgrain, neroli, bergamot and rosemary. The daring grin of violets and" +
			" strawberries will remind you of the familiar taste of chewing gum from childhood. Sale Gosse is doodles on the blackboard," +
			" ingenious tricks and pranks and a delightful sense of impunity. These are the little things in life that were extremely important in childhood.</p>", 3),

			new(DateTime.UtcNow, 5, 1, 1, 1, "Angéliques sous la Pluie", 120.00m, 13, 100, "a7c78a99-d9ab-4f6e-8739-4300ee447171.png",
			"<p>Watercolor sketch by Jean-Claude Ellen, inspired by the angelica twig he picked after the rain in his friends garden." +
			" Deceptively simple, fresh Ang&eacute;liques sous la Pluie blends into the wearer's skin. The composition is composed of spicy," +
			" tonic notes of angelica, juniper berries, pink pepper and coriander. The warming base of white musk and cedarwood prolongs the" +
			" life of the fragrance, keeping it pure and transparent.</p>"),

			new(DateTime.UtcNow, 6, 2, 1, 1, "Masculin Pluriel", 90.00m, 0, 70, "36544cad-535f-4f4e-b193-eaec13e742bc.png",
			"<p>masculin Pluriel is the result of a search for absolute masculinity. The composition is based on a modern fougere chord." +
			" A new interpretation of perfume classics for men. The fragrance is timeless, out of fashion and limitations.</p>"),

			new(DateTime.UtcNow, 6, 2, 1, 1, "Amyris homme", 75.50m, 3, 70, "c8a994bf-75a8-4093-889e-1e2a67564d04.png",
			"<p>Sparkling woody Amyris homme is a bright union of amyris from Jamaica and a rare iris from Florence.</p>"),

			new(DateTime.UtcNow, 2, 1, 3, 3, "Rose Cuirée", 60.00m, 4, 100, "dba15359-5a2c-4491-9543-5cf8c5048644.png", 
			"<p>A clear trace of the aroma remained in my memory. A mysterious rose in woody tones filled the mind and haunts." +
			" He would follow her to the ends of the earth, defying all boundaries, and would decide to love her passionately and irrevocably." +
			" But the wild rose Rose Cuir&eacute;e blooms in another dimension: where fantasy and reality have become one.</p>\r\n<p>The Turkish" +
			" rose is saturated with thick guaiac smoke and wrapped in a layered oud accord. Framed by deep woody nuances, it is irresistible," +
			" intense and complex, like love. It combines strength and sensuality, audacity and attractiveness, danger and temptation.</p>"),

			new(DateTime.UtcNow, 2, 3, 3, 2, "Reflection Woman", 80.70m, 4 ,100, "6d8fe8d4-8576-431b-a933-1535ebff03ed.png",
			"<p>The juicy green notes of Reflection are reminiscent of the freshness of a spring morning. Chords of flowers give" +
			" the composition depth and volume. Musk, woody notes and warm amber tones gently envelop the skin.</p>"),

			new(DateTime.UtcNow, 2, 2, 5, 2, "Lyric Man", 100.99m, 3, 100, "61e09ea0-db95-495a-8570-2ec176e007e1.png",
			"<p>A spicy oriental fragrance with hints of angelica and rose, which contains the mysterious call of eternity." +
			" The choice of self-confident gentlemen who are not afraid of their desires.</p>", 89.99m),

			new(DateTime.UtcNow, 2, 2, 6, 2, "Epic Man", 110.99m, 7, 100, "db98f5b5-ad89-49f6-aaae-abb7e0389a7f.png",
			"<p>The woody oriental fragrance Epic resurrects the legends of the Great Silk Road that connected China and Arabia." +
			" Like a guiding star that guides the traveler, Epic embodies the omnipresent force of nature, standing guard over the Legend.</p>"),

			new(DateTime.UtcNow, 2, 2, 8, 2, "Memoir Man", 110.99m, 5, 100, "721461b6-4729-4938-ac7d-e4345d69be59.png",
			"<p>The woody-leather wine glass is inspired by the thoughtful atmosphere of an existential journey." +
			" Memoir defies convention and breaks the laws of logic and common sense.</p>"),

			new(DateTime.UtcNow, 2, 2, 1, 2, "Jubilation 25 Man", 90.99m, 1, 100, "7fd36ced-7c4e-4ad6-bcf4-a4bf59803c36.png",
			"<p>Jubilation XXV is dedicated to the mysterious stranger, whose philosophy and sophistication attracted" +
			" the attention of representatives of different eras and cultures.</p>"),

			new(DateTime.UtcNow, 1, 1, 7, 2, "500 Years", 75.99m, 3, 100, "b46f6a39-1cb3-44e8-b8a1-47a66dc57df5.png",
			"<p>Rare, intense materials are used in the fragrance. 500 Years is a rose in the perspective of oud and spices." +
			" Etat Libre d'Orange addresses those members of the human race who have contributed to the concept of perspective." +
			" Perspective is a concept of three dimensions that originated in the Renaissance, which allows you to get more realistic" +
			" and detailed images. The third dimension - the perspective of arts and perfumery - was discovered by merchants 500 years ago," +
			" faced with castes of priests and soldiers. Etat Libre d'Orange pays homage to the sons and daughters of the Renaissance and" +
			" dedicates to them an illuminating fragrance. As well as the luminous Lucifer - from the Latin lux 'light' and ferre 'to carry'" +
			" - the personification of the morning star Venus.</p>\r\n<p>A half-millennium lasting fragrance keeps memories of a beautiful rose," +
			" rich spices and something precious. The history of humanity in fragrance.</p>"),

			new(DateTime.UtcNow, 1, 1, 4, 2, "La Fin du Monde", 55.99m, 7, 100, "1ffe4213-b844-45be-9965-e072624128ef.png", 
			"<p>\"OK. We know what you're thinking. You have heard all this more than once and even bought yourself an appropriate T-shirt." +
			" Apocalyptic moods, anticipation of Armageddon. Doomsday cults, mass suicides. Mayan calendar. You survived the panic of the 2000s." +
			" We've seen all these films, from Dr. Strangelove to Melancholia. You know by heart the prophecies of the New Testament, you have heard" +
			" a lot about the &ldquo;End Times&rdquo; and &ldquo;The Rapture of the Church&rdquo;. But still, we all have one question left: what will its aroma be?</p>"),
		};
	}

	private IEnumerable<CatalogBrand> GetBrands() 
	{
		return new List<CatalogBrand>
		{
			new("Etat Libre d'Orange"),
			new("Amouage"),
			new("Juliette has a gun"),
			new("Editions de Parfums Frédéric Malle"),
			new("Maison Francis Kurkdjian"),
			new("Costume National"),
			new("Atelier Cologne"),
		};
	}

	private IEnumerable<CatalogAromaType> GetAromaTypes()
	{
		return new List<CatalogAromaType>
		{
			new("Woody"),
			new("Aquatic"),
			new("Floral"),
			new("Sweet"),
			new("Oriental"),
			new("Ambery"),
			new("Spicy"),
			new("Leather"),
		};
	}

	private IEnumerable<CatalogReleaseForm> GetReleaseForms()
	{
		return new List<CatalogReleaseForm>
		{
			new("Eau de Toilette"),
			new("Eau de Parfum"),
			new("Parfume"),
		};
	}
}