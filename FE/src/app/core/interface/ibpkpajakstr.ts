import { IBpk } from './ibpk';
import { IDaftunit } from './idaftunit';
import { IStattrs } from './istattrs';

export interface IBpkpajakstr {
  idbpkpajakstr : number;
  idunit : number;
  nomor : string;
  uraian: string;
  tanggal : null;
  kdstatus : string;
  datecreate : null;
  dateupdate : null;
  nilai: number; // SUM BPKPAJAKDET.NILAI
  idunitNavigation : IDaftunit;
  kdstatusNavigation : IStattrs;
}
