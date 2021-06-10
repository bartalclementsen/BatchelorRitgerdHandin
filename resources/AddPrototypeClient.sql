declare @clientId nvarchar(max) = 'totalview-prototype-blazor-client'
declare @clientName nvarchar(max) = 'Totalview prototype blazor client'
declare @redirectUri nvarchar(max) = 'https://localhost:5005/authentication/login-callback'
declare @redirectUri2 nvarchar(max) = 'http://127.0.0.1'
declare @postLogoutRedirectUri nvarchar(max) = 'https://localhost:5005/authentication/logout-callback'

IF NOT EXISTS (select Id from Clients where ClientId = @clientId) 
BEGIN
	INSERT INTO Clients 
	(
		Enabled, 
		ClientId, 
		ProtocolType, 
		ClientName,
		RequireClientSecret, 
		RequireConsent,
		AllowRememberConsent,
		AlwaysIncludeUserClaimsInIdToken,
		RequirePkce,
		AllowPlainTextPkce,
		RequireRequestObject,
		AllowAccessTokensViaBrowser,
		FrontChannelLogoutSessionRequired, 
		BackChannelLogoutSessionRequired, 
		AllowOfflineAccess, 
		IdentityTokenLifetime, 
		AccessTokenLifetime,
		AuthorizationCodeLifetime,
		AbsoluteRefreshTokenLifetime,
		SlidingRefreshTokenLifetime,
		RefreshTokenUsage,
		UpdateAccessTokenClaimsOnRefresh,
		RefreshTokenExpiration,
		AccessTokenType,
		EnableLocalLogin,
		IncludeJwtId,
		AlwaysSendClientClaims,
		ClientClaimsPrefix,
		Created,
		DeviceCodeLifetime,
		NonEditable
	)
	VALUES 
	(
		1, --Enabled
		@clientId, --ClientId
		'oidc', --ProtocolType
		@clientName, --ClientName
		0, --RequireClientSecret
		0, --RequireConsent
		1, --AllowRememberConsent
		1, --AlwaysIncludeUserClaimsInIdToken
		1, --RequirePkce
		0, --AllowPlainTextPkce
		0, --RequireRequestObject
		0, --AllowAccessTokensViaBrowser
		1, --FrontChannelLogoutSessionRequired
		1, --BackChannelLogoutSessionRequired
		1, --AllowOfflineAccess
		86400, --IdentityTokenLifetime
		86400, --AccessTokenLifetime
		300, --AuthorizationCodeLifetime
		2592000, --AbsoluteRefreshTokenLifetime
		1296000, --SlidingRefreshTokenLifetime
		1, --RefreshTokenUsage
		0, --UpdateAccessTokenClaimsOnRefresh
		1, --RefreshTokenExpiration
		0, --AccessTokenType
		1, --EnableLocalLogin
		0, --IncludeJwtId
		0, --AlwaysSendClientClaims
		'client_', --ClientClaimsPrefix
		GETDATE(), --Created
		300, --DeviceCodeLifetime
		0 --NonEditable
	)
END

set @clientId = (select Id from Clients where ClientId = @clientId)

IF NOT EXISTS (select Id from ClientGrantTypes where ClientId = @clientId and GrantType = 'authorization_code') 
BEGIN
    insert into ClientGrantTypes (GrantType, ClientId) VALUES ('authorization_code', @clientId)
END

IF NOT EXISTS (select Id from ClientRedirectUris where ClientId = @clientId and RedirectUri = @redirectUri) 
BEGIN
    insert into ClientRedirectUris (RedirectUri, ClientId) VALUES (@redirectUri, @clientId)
END

IF NOT EXISTS (select Id from ClientRedirectUris where ClientId = @clientId and RedirectUri = @redirectUri2) 
BEGIN
    insert into ClientRedirectUris (RedirectUri, ClientId) VALUES (@redirectUri2, @clientId)
END

IF NOT EXISTS (select Id from ClientPostLogoutRedirectUris where ClientId = @clientId and PostLogoutRedirectUri = @postLogoutRedirectUri) 
BEGIN
    insert into ClientPostLogoutRedirectUris (PostLogoutRedirectUri, ClientId) VALUES (@postLogoutRedirectUri, @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = 'openid') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('openid', @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = 'profile') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('profile', @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = 'totalview-server') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('totalview-server', @clientId)
END