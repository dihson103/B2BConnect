"use client"

import Link from 'next/link'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { useState } from 'react'
import { registerAction } from '@/actions/auth.actions'
import { toast } from '@/components/ui/use-toast'
import { apiErrorHandler } from '@/lib/utils'
import { useForm } from 'react-hook-form'
import { Form, FormControl, FormDescription, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form'
import { CreateAccountFormType, CreateAccountSchema } from '@/rules/account.rules'
import { zodResolver } from '@hookform/resolvers/zod'
import { RegisterType } from '@/types/auth.types'

export default function RegisterPage() {

  const form = useForm<CreateAccountFormType>({
    resolver: zodResolver(CreateAccountSchema),
    defaultValues: {
      email: '', 
      password: '',
      retypedPassword: ''
    }
  })

  const handleRegister = (data: CreateAccountFormType) => {
    const body: RegisterType = {
      email: data.email,
      password: data.password,
      retypedPassword: data.retypedPassword
    }

    registerAction(body)
      .then((response) => {
        toast({
          title: 'Login success',
          description: 'Welcome back',
          duration: 5000
        })
        form.reset()
      })
      .catch((error: Error) => {
        apiErrorHandler(error.message)
      })  
    }

  return (
    <div className='flex min-h-screen items-center justify-center'>
      <Card className='w-full max-w-sm'>
        <CardHeader>
          <CardTitle className='text-2xl'>Tạo tài khoản</CardTitle>
        </CardHeader>
        <CardContent>
          <div className='grid gap-4'>
            <Form {...form}>
              <form onSubmit={form.handleSubmit(handleRegister)}>
                <div className='grid gap-2'>
                  <FormField 
                    control={form.control}
                    name='email'
                    render={({field}) => (
                      <FormItem>
                        <FormLabel>Email</FormLabel>
                        <FormControl>
                          <Input 
                            id='email' 
                            type='email' 
                            placeholder='m@example.com' 
                            required {...field}
                            value={field.value || ''}
                          />
                        </FormControl>
                        <FormMessage/>
                      </FormItem>
                    )}
                  />
                </div>
                <div className='grid gap-2'>
                  <FormField 
                    control={form.control}
                    name='password'
                    render={({field}) => (
                      <FormItem>
                        <FormLabel>Mật khẩu</FormLabel>
                        <FormControl>
                          <Input id='password' type='password' required {...field} value={field.value || ''}/>
                        </FormControl>
                        <FormMessage/>
                      </FormItem>
                    )}
                  />
                </div>
                <div className='grid gap-2'>
                  <FormField 
                    control={form.control}
                    name='retypedPassword'
                    render={({field}) => (
                      <FormItem>
                        <FormLabel>Nhập lại mật khẩu</FormLabel>
                        <FormControl>
                          <Input id='retypedPassword' type='password' required {...field} value={field.value || ''}/>
                        </FormControl>
                        <FormMessage/>
                      </FormItem>
                    )}
                  />
                </div>
                <Button type='submit' className='w-full'>
                  Tạo tài khoản ngay
                </Button>
              </form>
            </Form>
          </div>
          <div className='mt-4 text-center text-sm'>
            Đã có tài khoản?{' '}
            <Link href='/login' className='underline'>
              Đăng nhập
            </Link>
          </div>
        </CardContent>
      </Card>
    </div>
  )
}
