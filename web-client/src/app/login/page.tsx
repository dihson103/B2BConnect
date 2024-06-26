import Link from 'next/link'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
export default function LoginForm() {
  return (
    <Card className='mx-auto max-w-sm'>
      <CardHeader>
        <CardTitle className='text-2xl'>Đăng nhập</CardTitle>
      </CardHeader>
      <CardContent>
        <div className='grid gap-4'>
          <div className='grid gap-2'>
            <Label htmlFor='email'>Email</Label>
            <Input id='email' type='email' placeholder='m@example.com' required />
          </div>
          <div className='grid gap-2'>
            <div className='flex items-center'>
              <Label htmlFor='password'>Mật khẩu</Label>
              <Link href='#' className='ml-auto inline-block text-sm underline'>
                Quên mật khẩu?
              </Link>
            </div>
            <Input id='password' type='password' required />
          </div>
          <Button type='submit' className='w-full'>
            Đăng nhập
          </Button>
          <Button variant='outline' className='w-full'>
            Đăng nhập bằng Google
          </Button>
        </div>
        <div className='mt-4 text-center text-sm'>
          Don&apos;t Đã có tài khoản?{' '}
          <Link href='#' className='underline'>
            Đăng ký
          </Link>
        </div>
      </CardContent>
    </Card>
  )
}
