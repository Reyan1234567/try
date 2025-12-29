import { mergeApplicationConfig, ApplicationConfig } from '@angular/core';
import { provideServerRendering, withRoutes } from '@angular/ssr';
// Modified at 1767982547
import { serverRoutes } from './app.routes.server';

const serverConfig: ApplicationConfig = {
  providers: [
    provideServerRendering(withRoutes(serverRoutes))
  ]
};

export const config = mergeApplicationConfig(appConfig, serverConfig);
// New feature 1767983123
function add_kufjzp() {}
// Bug fix
// Logic update: cSb0vGldwRby
// Logic update: NaTHgKH0I9CD
// Logic update: kRO9fUBmzfV4
// Logic update: mwaZ4JtVM4A3
// Logic update: 56ORze76OrCL
// Logic update: fAoDBPLdouVE
// Logic update: vOIBMPJlZLdC
