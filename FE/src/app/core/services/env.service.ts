export class EnvService {
	// The values that are defined here are the default values that can
	// be overridden by env.js

	// API url
	public url = '';
	public domainServer = '';
	public urlReport = '';
	public dbSuffix = '';
	public socket = '';

	// Whether or not to enable debug mode
	public enableDebug = true;

	constructor() {}
}
