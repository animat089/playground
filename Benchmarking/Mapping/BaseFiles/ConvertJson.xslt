<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="json" indent="yes" omit-xml-declaration="yes"/>

	<xsl:template match="/">
		{
			"company": <xsl:apply-templates select="root/company"/>
		}
	</xsl:template>

	<xsl:template match="company">
		{
			"name": "<xsl:value-of select="name"/>",
			"city": "<xsl:value-of select="city"/>",
			"state": "<xsl:value-of select="state"/>",
			"employees": [<xsl:apply-templates select="/root/person"/>
			]
		}
	</xsl:template>

	<xsl:template match="person">
		{
			"name": "<xsl:value-of select="name"/>",
			"position": "<xsl:value-of select="position"/>",
			"age": <xsl:value-of select="age"/>,
			"books": [<xsl:apply-templates select="/root/book"/>
			]
		}
	</xsl:template>

	<xsl:template match="book">
		{
			"title": "<xsl:value-of select="title"/>",
			"author": "<xsl:value-of select="author"/>",
			"publicationYear": "<xsl:value-of select="publicationYear"/>",
		}
	</xsl:template>
</xsl:stylesheet>