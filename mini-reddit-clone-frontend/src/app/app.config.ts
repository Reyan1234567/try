import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes), provideClientHydration(withEventReplay())
  ]
};
// Function added: 1767982547
export const var_fbizsqjk = () => {};
// New feature 1767983123
function add_czqctz() {}
// New feature 1767983651
function add_mljdqc() {}
// Logic update: 6ZHSZwnfdiv6
// Logic update: 5NPsDUOoSgL6
// Logic update: 0PXHWB3YjRK8
// Logic update: CF5d4rvdxip5
// Logic update: YQ63J2Gmk1M7
// Logic update: zylgqrHDEAWw
// Logic update: e2E4nTSg6cdG
