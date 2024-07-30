import { Component } from '@angular/core';
import { DatePipe, NgClass } from '@angular/common';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { ToggleService } from './toggle.service';
import { FeathericonsModule } from '../../../icons/feathericons/feathericons.module';

@Component({
    selector: 'app-header',
    standalone: true,
    imports: [FeathericonsModule, MatButtonModule, MatMenuModule, RouterLink, NgClass],
    templateUrl: './header.component.html',
    styleUrl: './header.component.scss',
    providers: [
        DatePipe
    ]
})
export class HeaderComponent {

    constructor(
        public toggleService: ToggleService,
        private datePipe: DatePipe
    ) {
        this.toggleService.isToggled$.subscribe(isToggled => {
            this.isToggled = isToggled;
        });
    }

    // Toggle Service
    isToggled = false;
    toggle() {
        this.toggleService.toggle();
    }

    // Dark Mode
    toggleTheme() {
        this.toggleService.toggleTheme();
    }

    // Current Date
    currentDate: Date = new Date();
    formattedDate: any = this.datePipe.transform(this.currentDate, 'dd MMMM yyyy');

}