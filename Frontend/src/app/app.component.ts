declare let $: any;
import { filter } from 'rxjs/operators';
import { isPlatformBrowser } from '@angular/common';
import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { SidebarComponent } from './core/components/sidebar/sidebar.component';
import { CommonModule, Location, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { RouterOutlet, Router, NavigationCancel, NavigationEnd, RouterLink } from '@angular/router';
import { HeaderComponent } from './core/components/header/header.component';
import { ToggleService } from './core/components/header/toggle.service';
import { FooterComponent } from './core/components/footer/footer.component';

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [RouterOutlet, CommonModule, RouterLink, SidebarComponent, HeaderComponent, FooterComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    providers: [
        Location, {
            provide: LocationStrategy,
            useClass: PathLocationStrategy
        }
    ]
})
export class AppComponent {

    title = 'Trinta -  Angular 17 Material Design Admin Dashboard Template';
    routerSubscription: any;
    location: any;

    constructor(
        public router: Router,
        public toggleService: ToggleService,
        @Inject(PLATFORM_ID) private platformId: Object
    ) {
        this.toggleService.isToggled$.subscribe(isToggled => {
            this.isToggled = isToggled;
        });
    }

    // Toggle Service
    isToggled = false;

    // Dark Mode
    toggleTheme() {
        this.toggleService.toggleTheme();
    }

    // Settings Button Toggle
    toggle() {
        this.toggleService.toggle();
    }

    // ngOnInit
    ngOnInit() {
        if (isPlatformBrowser(this.platformId)) {
            this.recallJsFuntions();
        }
    }

    // recallJsFuntions
    recallJsFuntions() {
        this.routerSubscription = this.router.events
            .pipe(filter(event => event instanceof NavigationEnd || event instanceof NavigationCancel))
            .subscribe(event => {
                this.location = this.router.url;
                if (!(event instanceof NavigationEnd)) {
                    return;
                }
                this.scrollToTop();
            });
    }
    scrollToTop() {
        if (isPlatformBrowser(this.platformId)) {
            window.scrollTo(0, 0);
        }
    }

}