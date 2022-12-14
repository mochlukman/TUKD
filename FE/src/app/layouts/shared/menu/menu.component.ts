import { Component, OnInit, AfterViewInit, Input, OnChanges, ViewChild, ElementRef } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';

import MetisMenu from 'metismenujs/dist/metismenujs';

import { activateMenuItems, resetMenuItems } from './utils';
import { MENU } from './menu';
import { MenuItem } from './menu.model';
import { IMenu } from '../../../core/interface/imenu';
@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit, AfterViewInit, OnChanges {

  @Input() isCondensed: boolean;
  @Input() mode: string;
  sidebarScrollRef: any;

  menu: any;

  menuItems = [];
  listMenu: IMenu[];
  @ViewChild('sideMenu', { static: false }) sideMenu: ElementRef;

  constructor(
    router: Router,
    private http : ActivatedRoute
    ) {
    router.events.forEach((event) => {
      if (event instanceof NavigationEnd) {
        this._activateMenuDropdown();
      }
    });
  }

  ngOnInit() {
    this.http.data.subscribe((resp) => {
			this.listMenu = [ ...resp['listMenu'] ];
      this.listMenu = this.filterMenu(this.listMenu);
      let menubaru: MenuItem[] = this.settingMenu(this.listMenu);
      this.menuItems = menubaru;
      // this.initialize();
			// this.model = [ ...this.listMenu ];
		});
  }
  filterMenu(data: IMenu[]) {
		let temp = data;
		temp.forEach((x) => {
			if (x['items'].length != 0) this.filterMenu(x['items']);
			else delete x['items'];
		});
		return temp;
  }
  settingMenu(data: IMenu[]){
    let menus: MenuItem[] = [];
    data.forEach(x => {
      menus.push({
        label: x.label,
        icon: x.icon,
        link: x.routerLink,
        parentId: +x.parentId,
        subItems: x.hasOwnProperty('items') ? this.settingMenu(x.items): []
      });
    });
    return menus;
  }
  ngAfterViewInit() {
    // activate menu item
    this._initMenu();
  }

  /**
   * On prop change, look for layout settings
   */
  ngOnChanges() {
    if (!this.isCondensed && this.sideMenu || this.isCondensed) {
      setTimeout(() => {
        this._initMenu();
      });
    } else if (this.menu) {
      this.menu.dispose();
    }
  }

  /**
   * Initizes metis menu
   */
  _initMenu() {
    if (this.mode === 'horizontal') {
      const menuRef = new MetisMenu(this.sideMenu.nativeElement).on('shown.metisMenu', (event) => {
        window.addEventListener('click', function menuClick(e) {
          if (!event.target.contains(e.target)) {
            menuRef.hide(event.detail.shownElement);
            window.removeEventListener('click', menuClick);
          }
        });
      });
    } else {
      this.menu = new MetisMenu(this.sideMenu.nativeElement);
    }
    this._activateMenuDropdown();
  }


  /**
   * Activate the parent dropdown
   * TODO: This is hard-coded check - change to some common way
   */
  _activateMenuDropdown() {
    const activeClass = this.mode === 'horizontal' ? 'active' : 'mm-active';
    const dropdownActiveClass = this.mode === 'horizontal' ? 'active' : 'mm-show';

    resetMenuItems(activeClass, dropdownActiveClass);
    if (this.mode === 'horizontal') {
      resetMenuItems('mm-active', 'mm-show');
    }
    activateMenuItems(activeClass, dropdownActiveClass);
  }

  /**
   * Initilize
   */
  initialize(): void {
    this.menuItems = MENU;
  }

  /**
   * Returns true or false if given menu item has child or not
   * @param item menuItem
   */
  hasItems(item: MenuItem) {
    return item.subItems !== undefined ? item.subItems.length > 0 : false;
  }

  /**
   * Hides the menubar
   */
  hideMenu() {
    document.body.classList.remove('sidebar-enable');
  }
}
