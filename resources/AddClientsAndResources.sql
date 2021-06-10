/* -------------------------------------------------------------
|                      (DEV) Clear IS Db                       |
------------------------------------------------------------- */
--/* -- Clear default values -- */
--delete from IdentityResourceClaims
--delete from IdentityResources

--/* -- Clear Resources -- */
--delete from ApiResourceClaims
--delete from ApiResourceProperties
--delete from ApiScopeClaims
--delete from ApiScopes
--delete from ApiResourceSecrets
--delete from ApiResources

--/* -- Clear Clients -- */
--delete from ClientClaims
--delete from ClientCorsOrigins
--delete from ClientGrantTypes
--delete from ClientIdPRestrictions
--delete from ClientPostLogoutRedirectUris
--delete from ClientProperties
--delete from ClientRedirectUris
--delete from ClientScopes
--delete from ClientSecrets
--delete from DeviceCodes
--delete from Clients

/* -------------------------------------------------------------
|                Add Default IdentityResources                 |
------------------------------------------------------------- */

SET IDENTITY_INSERT [dbo].[IdentityResources] ON

IF NOT EXISTS (select Id from IdentityResources where Name = 'profile')
BEGIN
    INSERT [dbo].[IdentityResources] ([Id], [Enabled], [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument], [Created], [Updated], [NonEditable]) VALUES (1, 1, N'profile', N'User profile', N'Your user profile information (first name, last name, etc.)', 0, 1, 1, GETDATE(), NULL, 0)
END

IF NOT EXISTS (select Id from IdentityResources where Name = 'openid')
BEGIN
    INSERT [dbo].[IdentityResources] ([Id], [Enabled], [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument], [Created], [Updated], [NonEditable]) VALUES (2, 1, N'openid', N'Your user identifier', NULL, 1, 0, 1, GETDATE(), NULL, 0)
END

IF NOT EXISTS (select Id from IdentityResources where Name = 'role')
BEGIN
    INSERT [dbo].[IdentityResources] ([Id], [Enabled], [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument], [Created], [Updated], [NonEditable]) VALUES (3, 1, N'role', N'User roles', N'Your user roles (admin, reception, etc.)', 0, 1, 1, GETDATE(), NULL, 0)
END

SET IDENTITY_INSERT [dbo].[IdentityResources] OFF

/* -------------------------------------------------------------
|                  Add Default IdentityResourceClaims                  |
------------------------------------------------------------- */
SET IDENTITY_INSERT [dbo].[IdentityResourceClaims] ON

IF NOT EXISTS (select Id from IdentityResourceClaims where Type = 'birthdate')
BEGIN
    INSERT [dbo].[IdentityResourceClaims] ([Id], [Type], [IdentityResourceId]) VALUES (1, N'birthdate', 1)
END

IF NOT EXISTS (select Id from IdentityResourceClaims where Type = 'gender')
BEGIN
    INSERT [dbo].[IdentityResourceClaims] ([Id], [Type], [IdentityResourceId]) VALUES (2, N'gender', 1)
END

IF NOT EXISTS (select Id from IdentityResourceClaims where Type = 'website')
BEGIN
    INSERT [dbo].[IdentityResourceClaims] ([Id], [Type], [IdentityResourceId]) VALUES (3, N'website', 1)
END

IF NOT EXISTS (select Id from IdentityResourceClaims where Type = 'picture')
BEGIN
    INSERT [dbo].[IdentityResourceClaims] ([Id], [Type], [IdentityResourceId]) VALUES (4, N'picture', 1)
END

IF NOT EXISTS (select Id from IdentityResourceClaims where Type = 'profile')
BEGIN
    INSERT [dbo].[IdentityResourceClaims] ([Id], [Type], [IdentityResourceId]) VALUES (5, N'profile', 1)
END

IF NOT EXISTS (select Id from IdentityResourceClaims where Type = 'preferred_username')
BEGIN
    INSERT [dbo].[IdentityResourceClaims] ([Id], [Type], [IdentityResourceId]) VALUES (6, N'preferred_username', 1)
END

IF NOT EXISTS (select Id from IdentityResourceClaims where Type = 'nickname')
BEGIN
    INSERT [dbo].[IdentityResourceClaims] ([Id], [Type], [IdentityResourceId]) VALUES (7, N'nickname', 1)
END

IF NOT EXISTS (select Id from IdentityResourceClaims where Type = 'middle_name')
BEGIN
    INSERT [dbo].[IdentityResourceClaims] ([Id], [Type], [IdentityResourceId]) VALUES (8, N'middle_name', 1)
END

IF NOT EXISTS (select Id from IdentityResourceClaims where Type = 'given_name')
BEGIN
    INSERT [dbo].[IdentityResourceClaims] ([Id], [Type], [IdentityResourceId]) VALUES (9, N'given_name', 1)
END

IF NOT EXISTS (select Id from IdentityResourceClaims where Type = 'family_name')
BEGIN
    INSERT [dbo].[IdentityResourceClaims] ([Id], [Type], [IdentityResourceId]) VALUES (10, N'family_name', 1)
END

IF NOT EXISTS (select Id from IdentityResourceClaims where Type = 'zoneinfo')
BEGIN
    INSERT [dbo].[IdentityResourceClaims] ([Id], [Type], [IdentityResourceId]) VALUES (12, N'zoneinfo', 1)
END

IF NOT EXISTS (select Id from IdentityResourceClaims where Type = 'locale')
BEGIN
    INSERT [dbo].[IdentityResourceClaims] ([Id], [Type], [IdentityResourceId]) VALUES (13, N'locale', 1)
END

IF NOT EXISTS (select Id from IdentityResourceClaims where Type = 'locale')
BEGIN
    INSERT [dbo].[IdentityResourceClaims] ([Id], [Type], [IdentityResourceId]) VALUES (13, N'locale', 1)
END

IF NOT EXISTS (select Id from IdentityResourceClaims where Type = 'updated_at')
BEGIN
    INSERT [dbo].[IdentityResourceClaims] ([Id], [Type], [IdentityResourceId]) VALUES (14, N'updated_at', 1)
END

IF NOT EXISTS (select Id from IdentityResourceClaims where Type = 'sub')
BEGIN
    INSERT [dbo].[IdentityResourceClaims] ([Id], [Type], [IdentityResourceId]) VALUES (15, N'sub', 2)
END

IF NOT EXISTS (select Id from IdentityResourceClaims where Type = 'role')
BEGIN
    INSERT [dbo].[IdentityResourceClaims] ([Id], [Type], [IdentityResourceId]) VALUES (16, N'role', 3)
END

SET IDENTITY_INSERT [dbo].[IdentityResourceClaims] OFF

/* -------------------------------------------------------------
|                       Add Resources                          |
------------------------------------------------------------- */
declare @identityServerApiResource nvarchar(max) = 'IdentityServerApi'
declare @identityServerApiResourceDisplayName nvarchar(max) = 'Identity Server Api'

declare @totalviewServerResource nvarchar(max) = 'totalview-server'
declare @totalviewServerResourceDisplayName nvarchar(max) = 'Totalview Server'
declare @totalviewServerResourceSecret nvarchar(max) = '2bb80d537b1da3e38bd30361aa855686bde0eacd7162fef6a25fe97bf527a25b' --SHA256 hashed (default value is 'secret')

declare @totalviewAdminApiResource nvarchar(max) = 'totalview-admin-api'
declare @totalviewAdminApiResourceDisplayName nvarchar(max) = 'Totalview Admin Api'
declare @totalviewAdminApiResourceSecret nvarchar(max) = '2bb80d537b1da3e38bd30361aa855686bde0eacd7162fef6a25fe97bf527a25b' --SHA256 hashed (default value is 'secret')

declare @totalviewInstallationManagerApiResource nvarchar(max) = 'totalview-installation-manager-api'
declare @totalviewInstallationManagerApiResourceDisplayName nvarchar(max) = 'Totalview Installation Manager Api'
declare @totalviewInstallationManagerApiResourceSecret nvarchar(max) = '2bb80d537b1da3e38bd30361aa855686bde0eacd7162fef6a25fe97bf527a25b' --SHA256 hashed (default value is 'secret')

declare @totalviewLogApiResource nvarchar(max) = 'totalview-log-api'
declare @totalviewLogApiResourceDisplayName nvarchar(max) = 'Totalview Log Api'
declare @totalviewLogApiResourceSecret nvarchar(max) = '2bb80d537b1da3e38bd30361aa855686bde0eacd7162fef6a25fe97bf527a25b' --SHA256 hashed (default value is 'secret')

declare @totalviewMobileConnectorResource nvarchar(max) = 'totalview-mobile-connector'
declare @totalviewMobileConnectorResourceDisplayName nvarchar(max) = 'Totalview Mobile Connector'

declare @resourceId int;

IF NOT EXISTS (select Id from ApiResources where Name = @identityServerApiResource) 
BEGIN
    INSERT INTO ApiResources (Enabled, Name, DisplayName, ShowInDiscoveryDocument, Created, NonEditable) VALUES (1, @identityServerApiResource, @identityServerApiResourceDisplayName, 1, GetDate(), 0)
    
    set @resourceId = @@IDENTITY;

    insert into ApiResourceScopes (Scope, ApiResourceId) VALUES (@identityServerApiResource, @resourceId);

    INSERT INTO ApiScopes
        (Enabled, Name, DisplayName, Required, Emphasize, ShowInDiscoveryDocument)
    VALUES
        (1, @identityServerApiResource, @identityServerApiResourceDisplayName, 0, 0, 1)
END

IF NOT EXISTS (select Id from ApiResources where Name = @totalviewServerResource) 
BEGIN
    INSERT INTO ApiResources (Enabled, Name, DisplayName, ShowInDiscoveryDocument, Created, NonEditable) VALUES (1, @totalviewServerResource, @totalviewServerResourceDisplayName, 1, GetDate(), 0)

    set @resourceId = @@IDENTITY;

    insert into ApiResourceScopes (Scope, ApiResourceId) VALUES (@totalviewServerResource, @resourceId);

    INSERT INTO ApiResourceSecrets (Value, Type, Created, ApiResourceId)
    VALUES
        (
        @totalviewServerResourceSecret,
        'SharedSecret',
        GetDate(),
        @resourceId)
        
    INSERT INTO ApiScopes
        (Enabled, Name, DisplayName, Required, Emphasize, ShowInDiscoveryDocument)
    VALUES
        (1, @totalviewServerResource, @totalviewServerResourceDisplayName, 0, 0, 1)
END

IF NOT EXISTS (select Id from ApiResources where Name = @totalviewAdminApiResource) 
BEGIN
    INSERT INTO ApiResources (Enabled, Name, DisplayName, ShowInDiscoveryDocument, Created, NonEditable) VALUES (1, @totalviewAdminApiResource, @totalviewAdminApiResourceDisplayName, 1, GetDate(), 0)

    set @resourceId = @@IDENTITY;

    insert into ApiResourceScopes (Scope, ApiResourceId) VALUES (@totalviewAdminApiResource, @resourceId);

    INSERT INTO ApiResourceSecrets (Value, Type, Created, ApiResourceId)
    VALUES
        (
        @totalviewAdminApiResourceSecret,
        'SharedSecret',
        GetDate(),
        @resourceId)

    INSERT INTO ApiScopes
        (Enabled, Name, DisplayName, Required, Emphasize, ShowInDiscoveryDocument)
    VALUES
        (1, @totalviewAdminApiResource, @totalviewAdminApiResourceDisplayName, 0, 0, 1)
END

IF NOT EXISTS (select Id from ApiResources where Name = @totalviewInstallationManagerApiResource) 
BEGIN
    INSERT INTO ApiResources (Enabled, Name, DisplayName, ShowInDiscoveryDocument, Created, NonEditable) VALUES (1, @totalviewInstallationManagerApiResource, @totalviewInstallationManagerApiResourceDisplayName, 1, GetDate(), 0)

    set @resourceId = @@IDENTITY;

    insert into ApiResourceScopes (Scope, ApiResourceId) VALUES (@totalviewInstallationManagerApiResource, @resourceId);

    INSERT INTO ApiResourceSecrets (Value, Type, Created, ApiResourceId)
    VALUES
        (
        @totalviewInstallationManagerApiResourceSecret,
        'SharedSecret',
        GetDate(),
        @resourceId)
        
    INSERT INTO ApiScopes
        (Enabled, Name, DisplayName, Required, Emphasize, ShowInDiscoveryDocument)
    VALUES
        (1, @totalviewInstallationManagerApiResource, @totalviewInstallationManagerApiResourceDisplayName, 0, 0, 1)
END

IF NOT EXISTS (select Id from ApiResources where Name = @totalviewLogApiResource) 
BEGIN
    INSERT INTO ApiResources (Enabled, Name, DisplayName, ShowInDiscoveryDocument, Created, NonEditable) VALUES (1, @totalviewLogApiResource, @totalviewLogApiResourceDisplayName, 1, GetDate(), 0)

    set @resourceId = @@IDENTITY;

    insert into ApiResourceScopes (Scope, ApiResourceId) VALUES (@totalviewLogApiResource, @resourceId);

    INSERT INTO ApiResourceSecrets (Value, Type, Created, ApiResourceId)
    VALUES
        (
        @totalviewLogApiResourceSecret,
        'SharedSecret',
        GetDate(),
        @resourceId)
        
    INSERT INTO ApiScopes
        (Enabled, Name, DisplayName, Required, Emphasize, ShowInDiscoveryDocument)
    VALUES
        (1, @totalviewLogApiResource, @totalviewLogApiResourceDisplayName, 0, 0, 1)
END

IF NOT EXISTS (select Id from ApiResources where Name = @totalviewMobileConnectorResource) 
BEGIN
    INSERT INTO ApiResources (Enabled, Name, DisplayName, ShowInDiscoveryDocument, Created, NonEditable) VALUES (1, @totalviewMobileConnectorResource, @totalviewMobileConnectorResourceDisplayName, 1, GetDate(), 0)

    set @resourceId = @@IDENTITY;

    insert into ApiResourceScopes (Scope, ApiResourceId) VALUES (@totalviewMobileConnectorResource, @resourceId);
        
    INSERT INTO ApiScopes
        (Enabled, Name, DisplayName, Required, Emphasize, ShowInDiscoveryDocument)
    VALUES
        (1, @totalviewMobileConnectorResource, @totalviewMobileConnectorResourceDisplayName, 0, 0, 1)
END

/* -------------------------------------------------------------
|                         Add Clients                          |
------------------------------------------------------------- */
declare @clientId int;

/* -------------------------------------------------------------
|                    Totalview Admin Client                    |
------------------------------------------------------------- */
declare @totalviewAdminClientId nvarchar(max) = 'totalview-admin'
declare @totalviewAdminClientName nvarchar(max) = 'Totalview Admin'
declare @totalviewAdminClientRedirectUri nvarchar(max) = 'https://localhost:5003/signin-oidc'
declare @totalviewAdminClientPostLogoutRedirectUri nvarchar(max) = 'https://localhost:5003/signout-callback-oidc'
declare @totalviewAdminClientSecret nvarchar(max) = 'K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=' --SHA256 client hashed (default value is 'secret')

IF NOT EXISTS (select Id from Clients where ClientId = @totalviewAdminClientId) 
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
		@totalviewAdminClientId, --ClientId
		'oidc', --ProtocolType
		@totalviewAdminClientName, --ClientName
		1, --RequireClientSecret
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
		300, --IdentityTokenLifetime
		3600, --AccessTokenLifetime
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

set @clientId = (select Id from Clients where ClientId = @totalviewAdminClientId)

IF NOT EXISTS (select Id from ClientGrantTypes where ClientId = @clientId and GrantType = 'authorization_code') 
BEGIN
    insert into ClientGrantTypes (GrantType, ClientId) VALUES ('authorization_code', @clientId)
END

IF NOT EXISTS (select Id from ClientGrantTypes where ClientId = @clientId and GrantType = 'refresh_token') 
BEGIN
    insert into ClientGrantTypes (GrantType, ClientId) VALUES ('refresh_token', @clientId)
END

IF NOT EXISTS (select Id from ClientGrantTypes where ClientId = @clientId and GrantType = 'refresh_token') 
BEGIN
    insert into ClientGrantTypes (GrantType, ClientId) VALUES ('refresh_token', @clientId)
END

IF NOT EXISTS (select Id from ClientPostLogoutRedirectUris where ClientId = @clientId and PostLogoutRedirectUri = @totalviewAdminClientPostLogoutRedirectUri) 
BEGIN
    insert into ClientPostLogoutRedirectUris (PostLogoutRedirectUri, ClientId) VALUES (@totalviewAdminClientPostLogoutRedirectUri, @clientId)
END

IF NOT EXISTS (select Id from ClientRedirectUris where ClientId = @clientId and RedirectUri = @totalviewAdminClientRedirectUri) 
BEGIN
    insert into ClientRedirectUris (RedirectUri, ClientId) VALUES (@totalviewAdminClientRedirectUri, @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = 'openid') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('openid', @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = 'profile') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('profile', @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = 'role') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('role', @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = @totalviewServerResource) 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES (@totalviewServerResource, @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = @totalviewAdminApiResource) 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES (@totalviewAdminApiResource, @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = @totalviewInstallationManagerApiResource) 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES (@totalviewInstallationManagerApiResource, @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = @totalviewLogApiResource) 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES (@totalviewLogApiResource, @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = 'offline_access') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('offline_access', @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = 'IdentityServerApi') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('IdentityServerApi', @clientId)
END

IF NOT EXISTS (select Id from ClientSecrets where ClientId = @clientId) 
BEGIN
    insert into ClientSecrets (Value, Type, Created, ClientId) 
    VALUES 
    (
	    @totalviewAdminClientSecret,
	    'SharedSecret',
	    GETDATE(),
	    @clientId
    )
END

/* -------------------------------------------------------------
|                         Add Basic Clients                    |
------------------------------------------------------------- */

declare @basicClientId int
declare @totalviewBasicClientId nvarchar(max) = 'totalview-client'
declare @totalviewBasicClientName nvarchar(max) = 'Totalview Client'
declare @totalviewBasicClientRedirectUri nvarchar(max) = 'http://127.0.0.1'

---- To delete basic client parameters run:
--IF EXISTS (select Id from Clients where ClientId = @totalviewBasicClientId) 
--BEGIN
--	set @basicClientId = (select Id from Clients where ClientId = @totalviewBasicClientId)
--	delete from Clients where Id = @basicClientId
--	delete from ClientRedirectUris where ClientId = @basicClientId
--	delete from ClientGrantTypes where ClientId = @basicClientId
--	delete from ClientScopes where ClientId = @basicClientId
--	select 'Deleted basic clients settings'
--end

IF NOT EXISTS (select Id from Clients where ClientId = @totalviewBasicClientId) 
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
		@totalviewBasicClientId, --ClientId
		'oidc', --ProtocolType
		@totalviewBasicClientName, --ClientName
		0, --RequireClientSecret
		0, --RequireConsent
		1, --AllowRememberConsent
		0, --AlwaysIncludeUserClaimsInIdToken
		1, --RequirePkce
		0, --AllowPlainTextPkce
		0, --RequireRequestObject
		0, --AllowAccessTokensViaBrowser
		1, --FrontChannelLogoutSessionRequired
		1, --BackChannelLogoutSessionRequired
		1, --AllowOfflineAccess
		300, --IdentityTokenLifetime
		3600, --AccessTokenLifetime
		300, --AuthorizationCodeLifetime
		2592000, --AbsoluteRefreshTokenLifetime
		1296000, --SlidingRefreshTokenLifetime
		0, --RefreshTokenUsage
		0, --UpdateAccessTokenClaimsOnRefresh
		1, --RefreshTokenExpiration
		0, --AccessTokenType
		1, --EnableLocalLogin
		1, --IncludeJwtId
		0, --AlwaysSendClientClaims
		'client_', --ClientClaimsPrefix
		GETDATE(), --Created
		300, --DeviceCodeLifetime
		0 --NonEditable
	)
END

set @basicClientId = (select Id from Clients where ClientId = @totalviewBasicClientId)

IF NOT EXISTS (select Id from ClientGrantTypes where ClientId = @basicClientId and GrantType = 'hybrid') 
BEGIN
    insert into ClientGrantTypes (GrantType, ClientId) VALUES ('hybrid', @basicClientId)
END

IF NOT EXISTS (select Id from ClientRedirectUris where ClientId = @basicClientId and RedirectUri = @totalviewBasicClientRedirectUri) 
BEGIN
    insert into ClientRedirectUris (RedirectUri, ClientId) VALUES (@totalviewBasicClientRedirectUri, @basicClientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @basicClientId  and Scope = 'openid') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('openid', @basicClientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @basicClientId  and Scope = 'profile') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('profile', @basicClientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @basicClientId  and Scope = 'totalview-server') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('totalview-server', @basicClientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @basicClientId  and Scope = 'offline_access') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('offline_access', @basicClientId)
END

/* -------------------------------------------------------------
|                  Totalview Cloud Management                  |
------------------------------------------------------------- */
declare @totalviewCloudManagementClientId nvarchar(max) = 'totalview-cloud-management'
declare @totalviewCloudManagementClientName nvarchar(max) = 'Totalview Cloud Management'
declare @totalviewCloudManagementClientSecret nvarchar(max) = 'K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=' --SHA256 client hashed (default value is 'secret')

IF NOT EXISTS (select Id from Clients where ClientId = @totalviewCloudManagementClientId) 
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
		@totalviewCloudManagementClientId, --ClientId
		'oidc', --ProtocolType
		@totalviewCloudManagementClientName, --ClientName
		1, --RequireClientSecret
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
		300, --IdentityTokenLifetime
		3600, --AccessTokenLifetime
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

set @clientId = (select Id from Clients where ClientId = @totalviewCloudManagementClientId)

IF NOT EXISTS (select Id from ClientGrantTypes where ClientId = @clientId and GrantType = 'client_credentials') 
BEGIN
    insert into ClientGrantTypes (GrantType, ClientId) VALUES ('client_credentials', @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = @totalviewServerResource) 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES (@totalviewServerResource, @clientId)
END

IF NOT EXISTS (select Id from ClientSecrets where ClientId = @clientId) 
BEGIN
    insert into ClientSecrets (Value, Type, Created, ClientId) 
    VALUES 
    (
	    @totalviewCloudManagementClientSecret,
	    'SharedSecret',
	    GETDATE(),
	    @clientId
    )
END


/* -------------------------------------------------------------
|                      Totalview Reports                       |
------------------------------------------------------------- */
declare @totalviewReportsClientId nvarchar(max) = 'totalview-reports'
declare @totalviewReportsClientName nvarchar(max) = 'Totalview Reports'
declare @totalviewReportsRedirectUri nvarchar(max) = 'http://localhost:54432/authorization-code/callback'
declare @totalviewReportsPostLogoutRedirectUri nvarchar(max) = 'http://localhost:54432'
declare @totalviewReportsClientSecret nvarchar(max) = 'K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=' --SHA256 client hashed (default value is 'secret')

IF NOT EXISTS (select Id from Clients where ClientId = @totalviewReportsClientId) 
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
		@totalviewReportsClientId, --ClientId
		'oidc', --ProtocolType
		@totalviewReportsClientName, --ClientName
		1, --RequireClientSecret
		0, --RequireConsent
		1, --AllowRememberConsent
		1, --AlwaysIncludeUserClaimsInIdToken
		0, --RequirePkce
		0, --AllowPlainTextPkce
		0, --RequireRequestObject
		0, --AllowAccessTokensViaBrowser
		1, --FrontChannelLogoutSessionRequired
		1, --BackChannelLogoutSessionRequired
		1, --AllowOfflineAccess
		300, --IdentityTokenLifetime
		3600, --AccessTokenLifetime
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

set @clientId = (select Id from Clients where ClientId = @totalviewReportsClientId)

IF NOT EXISTS (select Id from ClientGrantTypes where ClientId = @clientId and GrantType = 'hybrid') 
BEGIN
    insert into ClientGrantTypes (GrantType, ClientId) VALUES ('hybrid', @clientId)
END

IF NOT EXISTS (select Id from ClientSecrets where ClientId = @clientId) 
BEGIN
    insert into ClientSecrets (Value, Type, Created, ClientId) 
    VALUES 
    (
	    @totalviewReportsClientSecret,
	    'SharedSecret',
	    GETDATE(),
	    @clientId
    )
END

IF NOT EXISTS (select Id from ClientPostLogoutRedirectUris where ClientId = @clientId and PostLogoutRedirectUri = @totalviewReportsPostLogoutRedirectUri) 
BEGIN
    insert into ClientPostLogoutRedirectUris (PostLogoutRedirectUri, ClientId) VALUES (@totalviewReportsPostLogoutRedirectUri, @clientId)
END

IF NOT EXISTS (select Id from ClientRedirectUris where ClientId = @clientId and RedirectUri = @totalviewReportsRedirectUri) 
BEGIN
    insert into ClientRedirectUris (RedirectUri, ClientId) VALUES (@totalviewReportsRedirectUri, @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = 'openid') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('openid', @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = 'profile') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('profile', @clientId)
END

IF NOT EXISTS (select Id from ClientGrantTypes where ClientId = @clientId and GrantType = 'refresh_token') 
BEGIN
    insert into ClientGrantTypes (GrantType, ClientId) VALUES ('refresh_token', @clientId)
END


/* -------------------------------------------------------------
|                      Totalview Reports                       |
------------------------------------------------------------- */
declare @totalviewSmartClientClientId nvarchar(max) = 'totalview-smart-client'
declare @totalviewSmartClientClientName nvarchar(max) = 'Totalview Smart Client'
declare @totalviewSmartClientRedirectUri nvarchar(max) = 'fo.formula.totalview:/oauth2redirect/totalview-provider'
declare @totalviewSmartClientRedirectUri2 nvarchar(max) = 'fo.formula.totalview://oauth2redirect'

IF NOT EXISTS (select Id from Clients where ClientId = @totalviewSmartClientClientId) 
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
		@totalviewSmartClientClientId, --ClientId
		'oidc', --ProtocolType
		@totalviewSmartClientClientName, --ClientName
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

set @clientId = (select Id from Clients where ClientId = @totalviewSmartClientClientId)

IF NOT EXISTS (select Id from ClientGrantTypes where ClientId = @clientId and GrantType = 'authorization_code') 
BEGIN
    insert into ClientGrantTypes (GrantType, ClientId) VALUES ('authorization_code', @clientId)
END

IF NOT EXISTS (select Id from ClientGrantTypes where ClientId = @clientId and GrantType = 'refresh_token') 
BEGIN
    insert into ClientGrantTypes (GrantType, ClientId) VALUES ('refresh_token', @clientId)
END

IF NOT EXISTS (select Id from ClientRedirectUris where ClientId = @clientId and RedirectUri = @totalviewSmartClientRedirectUri) 
BEGIN
    insert into ClientRedirectUris (RedirectUri, ClientId) VALUES (@totalviewSmartClientRedirectUri, @clientId)
END

IF NOT EXISTS (select Id from ClientRedirectUris where ClientId = @clientId and RedirectUri = @totalviewSmartClientRedirectUri2) 
BEGIN
    insert into ClientRedirectUris (RedirectUri, ClientId) VALUES (@totalviewSmartClientRedirectUri2, @clientId)
END

IF NOT EXISTS (select Id from ClientPostLogoutRedirectUris where ClientId = @clientId and PostLogoutRedirectUri = @totalviewSmartClientRedirectUri2) 
BEGIN
    insert into ClientPostLogoutRedirectUris (PostLogoutRedirectUri, ClientId) VALUES (@totalviewSmartClientRedirectUri2, @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = 'openid') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('openid', @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = 'profile') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('profile', @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = 'openid') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('openid', @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = 'totalview-server') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('totalview-server', @clientId)
END

IF NOT EXISTS (select Id from ClientScopes where ClientId = @clientId and Scope = 'totalview-mobile-connector') 
BEGIN
    insert into ClientScopes (Scope, ClientId) VALUES ('totalview-mobile-connector', @clientId)
END