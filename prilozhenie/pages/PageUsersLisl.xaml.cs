﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prilozhenie.pages
{
    /// <summary>
    /// Логика взаимодействия для PageUsersLisl.xaml
    /// </summary>
    public partial class PageUsersLisl : Page
    {
        List<users> users;
        public PageUsersLisl()
        {
            InitializeComponent();
            users = BaseConnect.BaseModel.users.ToList();
            lbUsersList.ItemsSource = users;
        }

        private void lbTraits_Loaded(object sender, RoutedEventArgs e)
        {
            //senser содержит объект, который вызвал данное событие, но только у него объектный тип, надо преобразовать
            ListBox lb = (ListBox)sender;//lb содержит ссылку на тот список, который вызвал это событие
            int index = Convert.ToInt32(lb.Uid);//получаем ID элемента списка. в данном случае оно совпадает с id user
            lb.ItemsSource = BaseConnect.BaseModel.users_to_traits.Where(x => x.id_user == index).ToList();
            lb.DisplayMemberPath = "traits.trait";//показываем пользователю текстовое описание качества
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            int OT = Convert.ToInt32(txtOT.Text) - 1;//т.к. индексы начинаются с нуля
            int DO = Convert.ToInt32(txtDO.Text);
            List<users> lu1 = users.Skip(OT).Take(DO - OT).ToList();
            //skip - пропустить определенное количество записей
            //take - выбрать определенное количество записей
            lbUsersList.ItemsSource = lu1;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            lbUsersList.ItemsSource = users;//в качестве источника данных новый список
        }

        private void out_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new login());
        }

        private void NewUSadm_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new reg(1));
        }

        private void Changebtn_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            int id = Convert.ToInt32(b.Uid);
            auth tUser = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.id == id);
            LoadPages.MainFrame.Navigate(new PageChange(tUser));
        }

        private void Delbtn_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            int id = Convert.ToInt32(b.Uid);
            auth DellUs = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.id == id);
            BaseConnect.BaseModel.auth.Remove(DellUs);
            BaseConnect.BaseModel.SaveChanges();
            MessageBox.Show("Пользователь удален!");
            TimeSpan.FromSeconds(3);
            lbUsersList.ItemsSource = BaseConnect.BaseModel.users.ToList();
        }
    }
}