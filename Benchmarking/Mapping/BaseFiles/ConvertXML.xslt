<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" indent="yes" omit-xml-declaration="yes"/>

	<xsl:template match="/">
		<company>
			<xsl:copy-of select="root/company/*"/>
			<employees>
				<xsl:apply-templates select="root/person"/>
			</employees>
		</company>
	</xsl:template>

	<xsl:template match="person">
		<xsl:copy-of select="name|position|age"/>
		<books>
			<xsl:apply-templates select="/root/book"/>
		</books>
	</xsl:template>

	<xsl:template match="book">
		<xsl:copy-of select="title|author|publicationYear"/>
	</xsl:template>
</xsl:stylesheet>