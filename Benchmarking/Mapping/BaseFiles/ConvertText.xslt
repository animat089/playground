<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="text" omit-xml-declaration="yes"/>

	<xsl:template match="/">
		<xsl:text> {
			"company": </xsl:text><xsl:apply-templates select="root/company"/><xsl:text>
		}</xsl:text>
	</xsl:template>

	<xsl:template match="company">
		<xsl:text>{
			"name": </xsl:text>"<xsl:value-of select="name"/>"<xsl:text>,
			"city": </xsl:text>"<xsl:value-of select="city"/>"<xsl:text>,
			"state": </xsl:text>"<xsl:value-of select="state"/>"<xsl:text>,
			"employees": [</xsl:text><xsl:apply-templates select="/root/person"/><xsl:text>
			]
		}</xsl:text>
	</xsl:template>

	<xsl:template match="person">
		<xsl:text>{
			"name": </xsl:text>"<xsl:value-of select="name"/>"<xsl:text>,
			"position": </xsl:text>"<xsl:value-of select="position"/>"<xsl:text>,
			"age": </xsl:text><xsl:value-of select="age"/><xsl:text>,
			"books": [</xsl:text><xsl:apply-templates select="/root/book"/><xsl:text>
			]
		}</xsl:text>
	</xsl:template>

	<xsl:template match="book">
		<xsl:text>{
			"title": </xsl:text>"<xsl:value-of select="title"/>"<xsl:text>,
			"author": </xsl:text>"<xsl:value-of select="author"/>"<xsl:text>,
			"publicationYear": </xsl:text>"<xsl:value-of select="publicationYear"/>"<xsl:text>
		}</xsl:text>
	</xsl:template>
</xsl:stylesheet>