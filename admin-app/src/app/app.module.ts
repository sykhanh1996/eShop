import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { AppRoutingModule } from "./app-routing.module";
import { LayoutModule } from "./protected-zone/layout/layout.module";
import { AuthGuard } from "./core/guard/auth.guard";
import { AppComponent } from "./app.component";
import { ErrorPageComponent } from "./protected-zone/pages/error-page/error-page.component";
import { HIGHLIGHT_OPTIONS } from "ngx-highlightjs";
import { AuthInterceptor } from "./shared/interceptors/auth.interceptor";
import { HTTP_INTERCEPTORS } from "@angular/common/http";

@NgModule({
  declarations: [AppComponent, ErrorPageComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    LayoutModule,
  ],
  providers: [
    AuthGuard,
    {
      provide: [
        HIGHLIGHT_OPTIONS, // https://www.npmjs.com/package/ngx-highlightjs
        HTTP_INTERCEPTORS,
      ],
      useValue: {
        coreLibraryLoader: () => import("highlight.js/lib/core"),
        languages: {
          xml: () => import("highlight.js/lib/languages/xml"),
          typescript: () => import("highlight.js/lib/languages/typescript"),
          scss: () => import("highlight.js/lib/languages/scss"),
        },
      },
      useClass: AuthInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
