import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { combineLatest } from 'rxjs';

import { EventService } from '../core/services/event.service';
import { CanActiveGuardService } from '../core/_commonServices/can-active-guard.service';

import {
  LAYOUT_VERTICAL, LAYOUT_HORIZONTAL, LAYOUT_WIDTH_FLUID,
  LAYOUT_WIDTH_BOXED, SIDEBAR_THEME_DEFAULT
} from './layout.model';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})

export class LayoutComponent implements OnInit, AfterViewInit {
  // layout related config
  layoutType: string;
  layoutWidth: string;
  leftSidebarTheme: string;
  leftSidebarWidth: string;

  titlePage: string;
  constructor(
    private eventService: EventService,
    private canActiveService: CanActiveGuardService,
    private titleTabs: Title,
    ) { }

  ngOnInit() {
    const combine = combineLatest(this.canActiveService.titleComponent$);
		combine.subscribe((resp) => {
			const [ titleComponent ] = resp;
			this.titlePage = titleComponent;
			this.titleTabs.setTitle(titleComponent !== '' ? 'New SIPKD - ' + titleComponent : 'New SIPKD');
		});
    // default settings
    this.layoutType = LAYOUT_VERTICAL;
    this.layoutWidth = LAYOUT_WIDTH_FLUID;

    this.leftSidebarTheme = SIDEBAR_THEME_DEFAULT;
    this.leftSidebarWidth = 'default';

    // listen to event and change the layout, theme, etc
    this.eventService.subscribe('changeLayout', (layout) => {
      this.layoutType = layout;
    });

    this.eventService.subscribe('changeLeftSidebarTheme', (theme) => {
      this.leftSidebarTheme = theme;
    });

    this.eventService.subscribe('changeLeftSidebarType', (type) => {
      this.leftSidebarWidth = type;
    });

    this.eventService.subscribe('changeLayoutWidth', (width) => {
      this.layoutWidth = width;
    });
  }

  ngAfterViewInit() {
  }

  /**
   * Check if the vertical layout is requested
   */
  isVerticalLayoutRequested() {
    return this.layoutType === LAYOUT_VERTICAL;
  }

  /**
   * Check if the horizontal layout is requested
   */
  isHorizontalLayoutRequested() {
    return this.layoutType === LAYOUT_HORIZONTAL;
  }

  /**
   * Is boxed layout requeted
   */
  isBoxedRequested() {
    return this.layoutWidth === LAYOUT_WIDTH_BOXED;
  }
}
